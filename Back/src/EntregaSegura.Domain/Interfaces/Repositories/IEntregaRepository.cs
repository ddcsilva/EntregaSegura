using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Domain.Interfaces;

public interface IEntregaRepository : IRepositoryBase<Entrega>
{
    Task<IEnumerable<Entrega>> ObterTodasEntregasComMoradoresEUnidadesEFuncionariosETransportadorasAsync(bool rastrearAlteracoes = false);
}