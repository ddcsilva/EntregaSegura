using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Domain.Interfaces.Repositories;

public interface IMoradorRepository : IRepository<Morador>
{
    Task<Morador> ObterPorNomeAsync(string nome);
    Task<IEnumerable<Morador>> ObterMoradoresPorUnidadeAsync(int unidadeId);
    Task<Morador> ObterMoradorComEntregasAsync(int id);
    Task<Morador> ObterMoradorComUnidadeAsync(int id);
}