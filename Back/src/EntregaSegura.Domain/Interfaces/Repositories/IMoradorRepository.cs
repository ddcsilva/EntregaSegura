using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Domain.Interfaces;

public interface IMoradorRepository : IRepositoryBase<Morador>
{
    Task<IEnumerable<Morador>> ObterTodosMoradoresComUnidadeECondominioAsync(bool rastrearAlteracoes = false);
}