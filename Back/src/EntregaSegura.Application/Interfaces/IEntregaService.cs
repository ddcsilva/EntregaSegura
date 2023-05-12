using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Application.Interfaces;

public interface IEntregaService : IDisposable
{
    Task Adicionar(Entrega entrega);
    Task Atualizar(Entrega entrega);
    Task Remover(Guid id);
    Task<IEnumerable<Entrega>> ObterTodosAsync();
    Task<Entrega> ObterPorIdAsync(Guid id);
    Task<IEnumerable<Entrega>> ObterEntregasPorMoradorAsync(Guid moradorId);
    Task<IEnumerable<Entrega>> ObterEntregasPorFuncionarioAsync(Guid funcionarioId);
    Task<IEnumerable<Entrega>> ObterEntregasPorTransportadoraAsync(Guid transportadoraId);
    Task<IEnumerable<Entrega>> ObterEntregasComStatusAguardandoRetiradaPorMoradorAsync(Guid moradorId);
    Task<IEnumerable<Entrega>> ObterEntregasComStatusRetiradaPorMoradorAsync(Guid moradorId);
    Task<IEnumerable<Entrega>> ObterEntregasRecebidasPorFuncionarioAsync(Guid funcionarioId);
}