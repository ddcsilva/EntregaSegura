using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Domain.Interfaces.Repositories;

public interface IEntregaRepository : IRepository<Entrega>
{
    Task<IEnumerable<Entrega>> ObterEntregasPorMoradorAsync(int moradorId);
    Task<IEnumerable<Entrega>> ObterEntregasPorFuncionarioAsync(int funcionarioId);
    Task<IEnumerable<Entrega>> ObterEntregasPorTransportadoraAsync(int transportadoraId);
    Task<IEnumerable<Entrega>> ObterEntregasComStatusAguardandoRetiradaPorMoradorAsync(int moradorId);
    Task<IEnumerable<Entrega>> ObterEntregasComStatusRetiradaPorMoradorAsync(int moradorId);
    Task<IEnumerable<Entrega>> ObterEntregasRecebidasPorFuncionarioAsync(int funcionarioId);
}