using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Domain.Interfaces.Repositories;

public interface ICondominioRepository : IRepository<Condominio>
{
    Task<Condominio> ObterPorNomeAsync(string nome);
    Task<Condominio> ObterCondominioComUnidadesAsync(Guid id);
    Task<Condominio> ObterCondominioComFuncionariosAsync(Guid id);
}