using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Domain.Interfaces.Repositories;

public interface IMoradorRepository : IRepository<Morador>
{
    Task<Morador> ObterPorNomeAsync(string nome);
    Task<IEnumerable<Morador>> ObterMoradoresPorUnidadeAsync(Guid unidadeId);
    Task<Morador> ObterMoradorComEntregasAsync(Guid id);
    Task<Morador> ObterMoradorComUnidadeAsync(Guid id);
}