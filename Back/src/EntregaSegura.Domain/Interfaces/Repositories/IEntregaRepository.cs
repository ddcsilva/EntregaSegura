using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Domain.Interfaces;

public interface IEntregaRepository : IRepositoryBase<Entrega>
{
    Task<IEnumerable<Entrega>> ObterTodasEntregasComMoradoresEFuncionariosETransportadorasAsync(bool rastrearAlteracoes = false);
}