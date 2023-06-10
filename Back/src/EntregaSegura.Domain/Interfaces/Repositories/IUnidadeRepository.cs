using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Domain.Interfaces;

public interface IUnidadeRepository : IRepositoryBase<Unidade>
{
    Task<IEnumerable<Unidade>> ObterTodasUnidadesComCondominioAsync(bool rastrearAlteracoes = false);
}