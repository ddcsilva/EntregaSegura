using AutoMapper;
using EntregaSegura.Application.DTOs;
using EntregaSegura.Application.Interfaces;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Interfaces;
using EntregaSegura.Domain.Validations;

namespace EntregaSegura.Application.Services;

public class CondominioService : BaseService, ICondominioService
{
    private readonly ICondominioRepository _condominioRepository;
    private readonly IMapper _mapper;

    public CondominioService(
        ICondominioRepository condominioRepository,
        IMapper mapper,
        INotificadorErros notificadorErros) : base(notificadorErros)
    {
        _condominioRepository = condominioRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CondominioDTO>> ObterTodosCondominiosAsync()
    {
        var condominios = await _condominioRepository.BuscarTodosAsync();
        return _mapper.Map<IEnumerable<CondominioDTO>>(condominios);
    }

    public async Task<CondominioDTO> ObterCondominioPorIdAsync(int id)
    {
        var condominio = await _condominioRepository.BuscarPorIdAsync(id, rastrearAlteracoes: true);
        return _mapper.Map<CondominioDTO>(condominio);
    }

    public async Task<bool> AdicionarAsync(CondominioDTO condominioDTO)
    {
        var condominio = _mapper.Map<Condominio>(condominioDTO);

        if (!await ValidarCondominio(condominio)) return false;

        _condominioRepository.Adicionar(condominio);

        var adicionadoComSucesso = await _condominioRepository.SalvarAlteracoesAsync();

        if (!adicionadoComSucesso)
        {
            Notificar("Ocorreu um erro ao adicionar o condomínio.");
            return false;
        }

        condominioDTO.Id = condominio.Id;

        return true;
    }

    public async Task<bool> AtualizarAsync(CondominioDTO condominioDTO)
    {
        var condominio = _mapper.Map<Condominio>(condominioDTO);

        if (!await ValidarCondominio(condominio, ehAtualizacao: true)) return false;

        _condominioRepository.Adicionar(condominio);

        var atualizadoComSucesso = await _condominioRepository.SalvarAlteracoesAsync();

        if (!atualizadoComSucesso)
        {
            Notificar("Ocorreu um erro ao atualizar o condomínio.");
            return false;
        }

        return true;
    }

    public async Task<bool> RemoverAsync(int id)
    {
        var condominio = await _condominioRepository.BuscarPorIdAsync(id);

        if (condominio == null)
        {
            Notificar("Não foi possível encontrar o condomínio informado.");
            return false;
        }

        _condominioRepository.Remover(condominio);

        var removidoComSucesso = await _condominioRepository.SalvarAlteracoesAsync();

        if (!removidoComSucesso)
        {
            Notificar("Ocorreu um erro ao remover o condomínio.");
            return false;
        }

        return true;
    }

    public void Dispose()
    {
        _condominioRepository?.Dispose();
    }

    private async Task<bool> ValidarCondominio(Condominio condominio, bool ehAtualizacao = false)
    {
        if (!ExecutarValidacao(new CondominioValidator(), condominio)) return false;

        if (!string.IsNullOrWhiteSpace(condominio.Cnpj)
            && (await _condominioRepository.BuscarPorCondicaoAsync(c => c.Cnpj == condominio.Cnpj && (ehAtualizacao ? c.Id != condominio.Id : true))).Any())
        {
            Notificar("Já existe um condomínio com este CNPJ.");
            return false;
        }

        if (!string.IsNullOrWhiteSpace(condominio.Nome)
            && (await _condominioRepository.BuscarPorCondicaoAsync(c => c.Nome == condominio.Nome && (ehAtualizacao ? c.Id != condominio.Id : true))).Any())
        {
            Notificar("Já existe um condomínio com este Nome.");
            return false;
        }

        if (!string.IsNullOrWhiteSpace(condominio.Email)
            && (await _condominioRepository.BuscarPorCondicaoAsync(c => c.Email == condominio.Email && (ehAtualizacao ? c.Id != condominio.Id : true))).Any())
        {
            Notificar("Já existe um condomínio com este E-mail.");
            return false;
        }

        return true;
    }
}
