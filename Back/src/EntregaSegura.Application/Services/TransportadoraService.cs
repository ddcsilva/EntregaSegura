using AutoMapper;
using EntregaSegura.Application.DTOs;
using EntregaSegura.Application.Interfaces;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Interfaces;
using EntregaSegura.Domain.Validations;

namespace EntregaSegura.Application.Services;

public class TransportadoraService : BaseService, ITransportadoraService
{
    private readonly ITransportadoraRepository _transportadoraRepository;
    private readonly IMapper _mapper;

    public TransportadoraService(
        ITransportadoraRepository transportadoraRepository,
        IMapper mapper,
        INotificadorErros notificadorErros) : base(notificadorErros)
    {
        _transportadoraRepository = transportadoraRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TransportadoraDTO>> ObterTodasTransportadorasAsync()
    {
        var transportadoras = await _transportadoraRepository.BuscarTodos();
        return _mapper.Map<IEnumerable<TransportadoraDTO>>(transportadoras);
    }

    public async Task<TransportadoraDTO> ObterTransportadoraPorIdAsync(int id)
    {
        var transportadora = await _transportadoraRepository.BuscarPorIdAsync(id, rastrearAlteracoes: true);
        return _mapper.Map<TransportadoraDTO>(transportadora);
    }

    public async Task<bool> AdicionarAsync(TransportadoraDTO transportadoraDTO)
    {
        var transportadora = _mapper.Map<Transportadora>(transportadoraDTO);

        if (!await ValidarTransportadora(transportadora)) return false;

        _transportadoraRepository.Adicionar(transportadora);

        var adicionadoComSucesso = await _transportadoraRepository.SalvarAlteracoesAsync();

        if (!adicionadoComSucesso)
        {
            Notificar("Ocorreu um erro ao adicionar a transportadora.");
            return false;
        }

        transportadoraDTO.Id = transportadora.Id;

        return true;
    }

    public async Task<bool> AtualizarAsync(TransportadoraDTO transportadoraDTO)
    {
        var transportadora = _mapper.Map<Transportadora>(transportadoraDTO);

        if (!await ValidarTransportadora(transportadora, ehAtualizacao: true)) return false;

        _transportadoraRepository.Atualizar(transportadora);

        var atualizadoComSucesso = await _transportadoraRepository.SalvarAlteracoesAsync();

        if (!atualizadoComSucesso)
        {
            Notificar("Ocorreu um erro ao atualizar a transportadora.");
            return false;
        }

        return true;
    }

    public async Task<bool> RemoverAsync(int id)
    {
        var transportadora = await _transportadoraRepository.BuscarPorIdAsync(id, rastrearAlteracoes: true);

        if (transportadora == null)
        {
            Notificar("Transportadora não encontrada.");
            return false;
        }

        _transportadoraRepository.Remover(transportadora);

        var removidoComSucesso = await _transportadoraRepository.SalvarAlteracoesAsync();

        if (!removidoComSucesso)
        {
            Notificar("Ocorreu um erro ao remover a transportadora.");
            return false;
        }

        return true;
    }

    public void Dispose()
    {
        _transportadoraRepository?.Dispose();
    }

    private async Task<bool> ValidarTransportadora(Transportadora transportadora, bool ehAtualizacao = false)
    {
        if (!ExecutarValidacao(new TransportadoraValidator(), transportadora)) return false;

        if (!string.IsNullOrWhiteSpace(transportadora.Cnpj)
            && (await _transportadoraRepository.BuscarPorCondicao(c => c.Cnpj == transportadora.Cnpj && (ehAtualizacao ? c.Id != transportadora.Id : true))).Any())
        {
            Notificar("Já existe uma transportadora com este CNPJ.");
            return false;
        }

        if (!string.IsNullOrWhiteSpace(transportadora.Nome)
            && (await _transportadoraRepository.BuscarPorCondicao(c => c.Nome == transportadora.Nome && (ehAtualizacao ? c.Id != transportadora.Id : true))).Any())
        {
            Notificar("Já existe uma transportadora com este nome.");
            return false;
        }

        if (!string.IsNullOrWhiteSpace(transportadora.Email)
            && (await _transportadoraRepository.BuscarPorCondicao(c => c.Email == transportadora.Email && (ehAtualizacao ? c.Id != transportadora.Id : true))).Any())
        {
            Notificar("Já existe uma transportadora com este e-mail.");
            return false;
        }

        return true;
    }
}