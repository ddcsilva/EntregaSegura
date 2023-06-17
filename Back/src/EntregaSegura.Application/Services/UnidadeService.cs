using AutoMapper;
using EntregaSegura.Application.DTOs;
using EntregaSegura.Application.Interfaces;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Interfaces;
using EntregaSegura.Domain.Validations;

namespace EntregaSegura.Application.Services;

public class UnidadeService : BaseService, IUnidadeService
{
    private readonly IUnidadeRepository _unidadeRepository;
    private readonly ICondominioRepository _condominioRepository;
    private readonly IMoradorRepository _moradorRepository;
    private readonly IMapper _mapper;

    public UnidadeService(IUnidadeRepository unidadeRepository,
                          ICondominioRepository condominioRepository,
                          IMoradorRepository moradorRepository,
                          IMapper mapper,
                          INotificadorErros notificador) : base(notificador)
    {
        _unidadeRepository = unidadeRepository;
        _condominioRepository = condominioRepository;
        _moradorRepository = moradorRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UnidadeDTO>> ObterTodasUnidadesAsync()
    {
        var unidades = await _unidadeRepository.BuscarTodosAsync();
        return _mapper.Map<IEnumerable<UnidadeDTO>>(unidades);
    }

    public async Task<IEnumerable<UnidadeDTO>> ObterTodasUnidadesComCondominioAsync()
    {
        var unidades = await _unidadeRepository.ObterTodasUnidadesComCondominioAsync();
        return _mapper.Map<IEnumerable<UnidadeDTO>>(unidades);
    }

    public async Task<IEnumerable<UnidadeDTO>> ObterTodasUnidadesPorCondominioAsync(int condominioId)
    {
        var unidades = await _unidadeRepository.BuscarPorCondicaoAsync(u => u.CondominioId == condominioId);
        return _mapper.Map<IEnumerable<UnidadeDTO>>(unidades);
    }

    public async Task<UnidadeDTO> ObterUnidadePorIdAsync(int id, bool rastrearAlteracoes = false)
    {
        var unidade = await _unidadeRepository.BuscarPorIdAsync(id, rastrearAlteracoes);
        return _mapper.Map<UnidadeDTO>(unidade);
    }

    public async Task<bool> AdicionarAsync(UnidadeDTO unidadeDTO)
    {
        var unidade = _mapper.Map<Unidade>(unidadeDTO);

        if (!await ValidarUnidade(unidade)) return false;

        _unidadeRepository.Adicionar(unidade);

        var adicionadoComSucesso = await _unidadeRepository.SalvarAlteracoesAsync();

        if (!adicionadoComSucesso)
        {
            Notificar("Ocorreu um erro ao salvar a unidade.");
            return false;
        }

        unidadeDTO.Id = unidade.Id;

        return true;
    }

    public async Task<bool> AdicionarUnidadesEmMassaAsync(UnidadesEmMassaDTO unidadesDTO)
    {
        var unidadesExistentes = await _unidadeRepository.BuscarPorCondicaoAsync(u => u.CondominioId == unidadesDTO.CondominioId);

        if (unidadesExistentes.Any())
        {
            Notificar("Este condomínio já possui unidades associadas. A operação não pode ser completada.");
            return false;
        }

        for (int bloco = 1; bloco <= unidadesDTO.QuantidadeBlocos; bloco++)
        {
            for (int andar = 1; andar <= unidadesDTO.QuantidadeAndaresPorBloco; andar++)
            {
                for (int unidade = 1; unidade <= unidadesDTO.QuantidadeUnidadesPorAndar; unidade++)
                {
                    var unidadeParaAdicionar = new Unidade(unidade, andar, bloco.ToString(), unidadesDTO.CondominioId);
                    _unidadeRepository.Adicionar(unidadeParaAdicionar);
                }
            }
        }

        var adicionadoComSucesso = await _unidadeRepository.SalvarAlteracoesAsync();

        if (!adicionadoComSucesso)
        {
            Notificar("Ocorreu um erro ao salvar as unidades.");
            return false;
        }

        return true;
    }

    public async Task<bool> AtualizarAsync(UnidadeDTO unidadeDTO)
    {
        var unidade = _mapper.Map<Unidade>(unidadeDTO);

        if (!await ValidarUnidade(unidade, ehAtualizacao: true)) return false;

        _unidadeRepository.Atualizar(unidade);

        var atualizadoComSucesso = await _unidadeRepository.SalvarAlteracoesAsync();

        if (!atualizadoComSucesso)
        {
            Notificar("Ocorreu um erro ao atualizar a unidade.");
            return false;
        }

        return true;
    }

    public async Task<bool> RemoverAsync(int id)
    {
        var unidade = await _unidadeRepository.BuscarPorIdAsync(id);

        if (unidade == null)
        {
            Notificar("A unidade especificada não existe.");
            return false;
        }

        if (await TemAssociacoes(id))
        {
            Notificar("Esta unidade não pode ser removida pois existem registros associados a ela.");
            return false;
        }

        _unidadeRepository.Remover(unidade);

        var removidoComSucesso = await _unidadeRepository.SalvarAlteracoesAsync();

        if (!removidoComSucesso)
        {
            Notificar("Ocorreu um erro ao remover a unidade.");
            return false;
        }

        return true;
    }

    public void Dispose()
    {
        _unidadeRepository?.Dispose();
        _condominioRepository?.Dispose();
    }

    private async Task<bool> ValidarUnidade(Unidade unidade, bool ehAtualizacao = false)
    {
        if (!ExecutarValidacao(new UnidadeValidator(), unidade)) return false;

        var condominio = await _condominioRepository.BuscarPorIdAsync(unidade.CondominioId);
        if (condominio == null)
        {
            Notificar("O condomínio especificado não existe.");
            return false;
        }

        if (_unidadeRepository.BuscarPorCondicaoAsync(u => u.Numero == unidade.Numero && u.Andar == unidade.Andar && u.Bloco == unidade.Bloco && u.CondominioId == unidade.CondominioId && (!ehAtualizacao || u.Id != unidade.Id)).Result.Any())
        {
            Notificar("Já existe uma unidade com este número no mesmo andar, bloco e condomínio.");
            return false;
        }

        if (!_unidadeRepository.BuscarPorCondicaoAsync(u => u.CondominioId == unidade.CondominioId && u.Bloco == unidade.Bloco).Result.Any())
        {
            Notificar("O bloco especificado não existe neste condomínio.");
            return false;
        }

        return true;
    }

    private async Task<bool> TemAssociacoes(int unidadeId)
    {
        var moradores = await _moradorRepository.BuscarPorCondicaoAsync(m => m.UnidadeId == unidadeId);
        return moradores.Any();
    }
}