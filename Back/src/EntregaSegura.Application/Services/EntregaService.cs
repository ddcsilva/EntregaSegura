using EntregaSegura.Application.Interfaces;
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
                          INotificadorErros notificadorErros) : base(unitOfWork, notificadorErros)
    {
        _entregaRepository = entregaRepository;
    }

    public async Task<Entrega> Adicionar(Entrega entrega)
    {
        if (!ExecutarValidacao(new EntregaValidator(), entrega)) return null;

        await _entregaRepository.AdicionarAsync(entrega);
        var result = await CommitAsync();

        if (result == 0)
        {
            Notificar("Ocorreu um erro ao salvar a entrega.");
            return null;
        }

        return entrega;
    }

    public async Task<Entrega> Atualizar(Entrega entrega)
    {
        if (!ExecutarValidacao(new EntregaValidator(), entrega)) return null;

        _entregaRepository.Atualizar(entrega);
        var result = await CommitAsync();

        if (result == 0)
        {
            Notificar("Ocorreu um erro ao atualizar a entrega.");
            return null;
        }

        return entrega;
    }

    public async Task<bool> Remover(int id)
    {
        var entrega = await _entregaRepository.ObterPorIdAsync(id);

        if (entrega == null)
        {
            Notificar("Entrega não encontrada.");
            return false;
        }

        _entregaRepository.Remover(entrega);

        var result = await CommitAsync();

        if (result == 0)
        {
            Notificar("Ocorreu um erro ao remover a entrega.");
            return false;
        }

        return true;
    }

    public async Task<IEnumerable<Entrega>> ObterTodosAsync()
    {
        return await _entregaRepository.ObterTodosAsync();
    }

    public async Task<Entrega> ObterPorIdAsync(int id)
    {
        return await _entregaRepository.ObterPorIdAsync(id);
    }

    public async Task<IEnumerable<Entrega>> ObterEntregasPorMoradorAsync(int moradorId)
    {
        return await _entregaRepository.ObterEntregasPorMoradorAsync(moradorId);
    }

    public async Task<IEnumerable<Entrega>> ObterEntregasPorFuncionarioAsync(int funcionarioId)
    {
        return await _entregaRepository.ObterEntregasPorFuncionarioAsync(funcionarioId);
    }

    public async Task<IEnumerable<Entrega>> ObterEntregasPorTransportadoraAsync(int transportadoraId)
    {
        return await _entregaRepository.ObterEntregasPorTransportadoraAsync(transportadoraId);
    }

    public async Task<IEnumerable<Entrega>> ObterEntregasComStatusAguardandoRetiradaPorMoradorAsync(int moradorId)
    {
        return await _entregaRepository.ObterEntregasComStatusAguardandoRetiradaPorMoradorAsync(moradorId);
    }

    public async Task<IEnumerable<Entrega>> ObterEntregasComStatusRetiradaPorMoradorAsync(int moradorId)
    {
        return await _entregaRepository.ObterEntregasComStatusRetiradaPorMoradorAsync(moradorId);
    }

    public async Task<IEnumerable<Entrega>> ObterEntregasRecebidasPorFuncionarioAsync(int funcionarioId)
    {
        return await _entregaRepository.ObterEntregasRecebidasPorFuncionarioAsync(funcionarioId);
    }

    public void Dispose()
    {
        _unitOfWork?.Dispose();
    }
}