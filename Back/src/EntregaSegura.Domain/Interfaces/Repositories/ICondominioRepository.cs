using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Domain.Interfaces.Repositories;

public interface ICondominioRepository : IRepository<Condominio>
{
    Task<IEnumerable<Condominio>> ObterTodosCondominiosPorNomeAsync(string nome);
    Task<Condominio> ObterCondominioComUnidadesAsync(Guid id);
    Task<Condominio> ObterCondominioComFuncionariosAsync(Guid id);
    Task<Condominio> ObterCondominioComUnidadesEFuncionariosAsync(Guid id);
}