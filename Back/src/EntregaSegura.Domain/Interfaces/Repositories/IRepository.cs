using System.Linq.Expressions;
using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Domain.Interfaces.Repositories;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task AdicionarAsync(TEntity entity);
    void Atualizar(TEntity entity);
    void Remover(TEntity entity);
    void RemoverSerie(IEnumerable<TEntity> entities);
    Task<TEntity> ObterPorIdAsync(Guid id);
    Task<IEnumerable<TEntity>> ObterTodosAsync();
    Task<IEnumerable<TEntity>> BuscarAsync(Expression<Func<TEntity, bool>> predicate);
}
