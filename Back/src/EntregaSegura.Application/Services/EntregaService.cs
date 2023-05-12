using EntregaSegura.Application.Interfaces;
using EntregaSegura.Application.Notifications;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Interfaces.Repositories;
using EntregaSegura.Domain.Validators;
using EntregaSegura.Infrastructure.UnitOfWork;

namespace EntregaSegura.Application.Services;

public class EntregaService : BaseService, IEntregaService
{
    private readonly IEntregaRepository _entregaRepository;

    public EntregaService(IEntregaRepository entregaRepository,
                          IUnitOfWork unitOfWork,
                          NotificadorErros notificadorErros) : base(unitOfWork, notificadorErros)
    {
        _entregaRepository = entregaRepository;
    }

    public async Task Adicionar(Entrega entrega)
    {
        if (!ExecutarValidacao(new EntregaValidator(), entrega)) return;

        _entregaRepository.Adicionar(entrega);
        await CommitAsync();
    }

    public async Task Atualizar(Entrega entrega)
    {
        if (!ExecutarValidacao(new EntregaValidator(), entrega)) return;

        _entregaRepository.Atualizar(entrega);
        await CommitAsync();
    }

    public async Task Remover(Guid id)
    {
        _entregaRepository.Remover(id);
        await CommitAsync();
    }

    public async Task<IEnumerable<Entrega>> ObterTodosAsync()
    {
        return await _entregaRepository.ObterTodosAsync();
    }

    public async Task<Entrega> ObterPorIdAsync(Guid id)
    {
        return await _entregaRepository.ObterPorIdAsync(id);
    }

    public async Task<IEnumerable<Entrega>> ObterEntregasPorMoradorAsync(Guid moradorId)
    {
        return await _entregaRepository.ObterEntregasPorMoradorAsync(moradorId);
    }

    public async Task<IEnumerable<Entrega>> ObterEntregasPorFuncionarioAsync(Guid funcionarioId)
    {
        return await _entregaRepository.ObterEntregasPorFuncionarioAsync(funcionarioId);
    }

    public async Task<IEnumerable<Entrega>> ObterEntregasPorTransportadoraAsync(Guid transportadoraId)
    {
        return await _entregaRepository.ObterEntregasPorTransportadoraAsync(transportadoraId);
    }

    public async Task<IEnumerable<Entrega>> ObterEntregasComStatusAguardandoRetiradaPorMoradorAsync(Guid moradorId)
    {
        return await _entregaRepository.ObterEntregasComStatusAguardandoRetiradaPorMoradorAsync(moradorId);
    }

    public async Task<IEnumerable<Entrega>> ObterEntregasComStatusRetiradaPorMoradorAsync(Guid moradorId)
    {
        return await _entregaRepository.ObterEntregasComStatusRetiradaPorMoradorAsync(moradorId);
    }

    public async Task<IEnumerable<Entrega>> ObterEntregasRecebidasPorFuncionarioAsync(Guid funcionarioId)
    {
        return await _entregaRepository.ObterEntregasRecebidasPorFuncionarioAsync(funcionarioId);
    }

    public void Dispose()
    {
        _unitOfWork?.Dispose();
    }
}