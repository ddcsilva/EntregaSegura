using System.Linq.Expressions;
using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Domain.Interfaces.Repositories;

public interface IRepository<TEntity> : IDisposable where TEntity : BaseEntity
{
    Task AdicionarAsync(TEntity entity);
    Task AtualizarAsync(TEntity entity);
    Task RemoverAsync(Guid id);
    Task<TEntity> ObterPorIdAsync(Guid id);
    Task<IEnumerable<TEntity>> ObterTodosAsync();
    Task<IEnumerable<TEntity>> BuscarAsync(Expression<Func<TEntity, bool>> predicate);
    Task<int> SaveChangesAsync();
}