using AutoMapper;
using EntregaSegura.Application.DTOs;
using EntregaSegura.Application.Interfaces;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Interfaces;
using EntregaSegura.Domain.Validations;

namespace EntregaSegura.Application.Services;

public class EntregaService : BaseService, IEntregaService
{
    private readonly IEntregaRepository _entregaRepository;
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;

    public EntregaService(IEntregaRepository entregaRepository,
                          IEmailService emailService,
                          IMapper mapper,
                          INotificadorErros notificadorErros) : base(notificadorErros)
    {
        _entregaRepository = entregaRepository;
        _emailService = emailService;
        _mapper = mapper;
    }

    public Task<Entrega> Adicionar(Entrega entrega)
    {
        throw new NotImplementedException();
    }

    public Task<Entrega> Atualizar(Entrega entrega)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Entrega>> ObterEntregasComStatusAguardandoRetiradaPorMoradorAsync(int moradorId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Entrega>> ObterEntregasComStatusRetiradaPorMoradorAsync(int moradorId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Entrega>> ObterEntregasPorFuncionarioAsync(int funcionarioId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Entrega>> ObterEntregasPorMoradorAsync(int moradorId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Entrega>> ObterEntregasPorTransportadoraAsync(int transportadoraId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Entrega>> ObterEntregasRecebidasPorFuncionarioAsync(int funcionarioId)
    {
        throw new NotImplementedException();
    }

    public Task<Entrega> ObterPorIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Entrega>> ObterTodosAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> Remover(int id)
    {
        throw new NotImplementedException();
    }
}