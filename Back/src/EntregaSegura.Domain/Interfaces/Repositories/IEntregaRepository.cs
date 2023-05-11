using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Domain.Interfaces.Repositories;

public interface IEntregaRepository : IRepository<Entrega>
{
    Task<IEnumerable<Entrega>> ObterEntregasPorMoradorAsync(Guid moradorId);
    Task<IEnumerable<Entrega>> ObterEntregasPorFuncionarioAsync(Guid funcionarioId);
    Task<IEnumerable<Entrega>> ObterEntregasPorTransportadoraAsync(Guid transportadoraId);
    Task<IEnumerable<Entrega>> ObterEntregasComStatusPendentePorMoradorAsync(Guid moradorId);
    Task<IEnumerable<Entrega>> ObterEntregasComStatusRetiradaPorMoradorAsync(Guid moradorId);
    Task<IEnumerable<Entrega>> ObterEntregasRecebidasPorFuncionarioAsync(Guid funcionarioId);
}