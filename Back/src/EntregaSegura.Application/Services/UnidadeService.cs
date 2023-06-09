using AutoMapper;
using EntregaSegura.Application.DTOs;
using EntregaSegura.Application.Interfaces;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Interfaces;
using EntregaSegura.Domain.Validations;
using EntregaSegura.Infra.Data.UnitOfWork;

namespace EntregaSegura.Application.Services;

public class UnidadeService : BaseService, IUnidadeService
{
    private readonly IUnidadeRepository _unidadeRepository;
    private readonly ICondominioRepository _condominioRepository;
    private readonly IMapper _mapper;

    public UnidadeService(IUnidadeRepository unidadeRepository,
                          ICondominioRepository condominioRepository,
                          IMapper mapper,
                          IUnitOfWork unitOfWork,
                          INotificadorErros notificadorErros) : base(unitOfWork, notificadorErros)
    {
        _unidadeRepository = unidadeRepository;
        _condominioRepository = condominioRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UnidadeDTO>> ObterTodasUnidadesAsync(bool incluirCondominio, bool rastrearAlteracoes)
    {
        var unidades = await _unidadeRepository.ObterTodasUnidadesAsync(incluirCondominio, rastrearAlteracoes);
        return _mapper.Map<IEnumerable<UnidadeDTO>>(unidades);
    }

    public async Task<IEnumerable<UnidadeDTO>> ObterTodasUnidadesComCondominioAsync()
    {
        var unidades = await _unidadeRepository.ObterTodasUnidadesComCondominioAsync();
        return _mapper.Map<IEnumerable<UnidadeDTO>>(unidades);
    }

    public async Task<UnidadeDTO> ObterUnidadePorIdAsync(int id)
    {
        var unidade = await _unidadeRepository.ObterUnidadePorIdAsync(id);
        return _mapper.Map<UnidadeDTO>(unidade);
    }

    public async Task AdicionarAsync(UnidadeDTO unidadeDTO)
    {
        var unidade = _mapper.Map<Unidade>(unidadeDTO);

        if (!ExecutarValidacao(new UnidadeValidator(), unidade))
        {
            return;
        }

        var condominio = await _condominioRepository.ObterCondominioPorIdAsync(unidade.CondominioId);
        if (condominio == null)
        {
            Notificar("O condomínio especificado não existe.");
        }
        
        var unidadeExistente = _unidadeRepository.BuscarAsync(u => u.Numero == unidade.Numero && u.Andar == unidade.Andar && u.Bloco == unidade.Bloco && u.CondominioId == unidade.CondominioId).Result.Any();
        if (unidadeExistente)
        {
            Notificar("Já existe uma unidade com este número no mesmo andar, bloco e condomínio.");
        }

        var blocoExistente = _unidadeRepository.BuscarAsync(u => u.CondominioId == unidade.CondominioId && u.Bloco == unidade.Bloco).Result.Any();
        if (!blocoExistente)
        {
            Notificar("O bloco especificado não existe neste condomínio.");
        }

        if (TemNotificacoes())
        {
            return;
        }

        _unidadeRepository.Adicionar(unidade);
        var resultadoOperacao = await _unitOfWork.CommitAsync();

        if (resultadoOperacao == 0)
        {
            Notificar("Ocorreu um erro ao salvar a unidade.");
            return;
        }

        unidadeDTO.Id = unidade.Id;
    }

    public async Task<bool> AdicionarUnidadesEmMassaAsync(UnidadesEmMassaDTO unidadesDTO)
    {
        var unidadesExistentes = await _unidadeRepository.BuscarAsync(u => u.CondominioId == unidadesDTO.CondominioId);

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

        var resultadoOperacao = await _unitOfWork.CommitAsync();

        if (resultadoOperacao == 0)
        {
            Notificar("Ocorreu um erro ao salvar as unidades.");
        }

        return resultadoOperacao > 0;
    }

    public async Task AtualizarAsync(UnidadeDTO unidadeDTO)
    {
        var unidade = _mapper.Map<Unidade>(unidadeDTO);

        if (!ExecutarValidacao(new UnidadeValidator(), unidade))
        {
            return;
        }

        // var unidadeExistente = await _unidadeRepository.ObterUnidadePorIdAsync(unidadeDTO.Id);
        // if (unidadeExistente == null)
        // {
        //     Notificar("A unidade especificada não existe.");
        //     return;
        // }

        if (_unidadeRepository.BuscarAsync(u => u.Numero == unidade.Numero && u.Andar == unidade.Andar && u.Bloco == unidade.Bloco && u.CondominioId == unidade.CondominioId && u.Id != unidade.Id).Result.Any())
        {
            Notificar("Já existe uma unidade com este número no mesmo andar, bloco e condomínio.");
        }

        var condominio = await _condominioRepository.ObterCondominioPorIdAsync(unidade.CondominioId);
        if (condominio == null)
        {
            Notificar("O condomínio especificado não existe.");
        }

        var blocoExiste = _unidadeRepository.BuscarAsync(u => u.CondominioId == unidade.CondominioId && u.Bloco == unidade.Bloco).Result.Any();
        if (!blocoExiste)
        {
            Notificar("O bloco especificado não existe neste condomínio.");
        }

        if (TemNotificacoes())
        {
            return;
        }

        _unidadeRepository.Atualizar(unidade);
        var resultadoOperacao = await CommitAsync();

        if (resultadoOperacao == 0)
        {
            Notificar("Ocorreu um erro ao atualizar a unidade.");
        }
    }


    public async Task RemoverAsync(int id)
    {
        var unidadeExistente = await _unidadeRepository.ObterUnidadePorIdAsync(id);

        if (unidadeExistente == null)
        {
            Notificar("A unidade especificada não existe.");
            return;
        }

        _unidadeRepository.Remover(unidadeExistente);

        try
        {
            var resultadoOperacao = await _unitOfWork.CommitAsync();

            if (resultadoOperacao == 0)
            {
                Notificar("Ocorreu um erro ao remover a unidade.");
            }
        }
        catch (Exception)
        {
            Notificar("Ocorreu um erro inesperado. Favor, contate o administrador.");
        }
    }

    public void Dispose()
    {
        _unitOfWork?.Dispose();
    }
}