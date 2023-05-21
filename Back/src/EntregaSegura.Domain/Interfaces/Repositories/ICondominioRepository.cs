using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Domain.Interfaces.Repositories;

public interface ICondominioRepository : IRepository<Condominio>
{
    Task<IEnumerable<Condominio>> ObterTodosCondominiosPorNomeAsync(string nome);
    Task<Condominio> ObterCondominioComUnidadesAsync(int id);
    Task<Condominio> ObterCondominioComFuncionariosAsync(int id);
    Task<Condominio> ObterCondominioComUnidadesEFuncionariosAsync(int id);
}