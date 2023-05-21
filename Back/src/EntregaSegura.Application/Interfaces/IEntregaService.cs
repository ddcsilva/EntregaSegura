using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Application.Interfaces;

public interface IEntregaService : IDisposable
{
    Task<Entrega> Adicionar(Entrega entrega);
    Task<Entrega> Atualizar(Entrega entrega);
    Task<bool> Remover(int id);
    Task<IEnumerable<Entrega>> ObterTodosAsync();
    Task<Entrega> ObterPorIdAsync(int id);
    Task<IEnumerable<Entrega>> ObterEntregasPorMoradorAsync(int moradorId);
    Task<IEnumerable<Entrega>> ObterEntregasPorFuncionarioAsync(int funcionarioId);
    Task<IEnumerable<Entrega>> ObterEntregasPorTransportadoraAsync(int transportadoraId);
    Task<IEnumerable<Entrega>> ObterEntregasComStatusAguardandoRetiradaPorMoradorAsync(int moradorId);
    Task<IEnumerable<Entrega>> ObterEntregasComStatusRetiradaPorMoradorAsync(int moradorId);
    Task<IEnumerable<Entrega>> ObterEntregasRecebidasPorFuncionarioAsync(int funcionarioId);
}