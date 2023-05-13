using System.Linq.Expressions;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Interfaces.Repositories;
using EntregaSegura.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EntregaSegura.Infrastructure.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity, new()
{
    protected readonly EntregaSeguraContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public Repository(EntregaSeguraContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> BuscarAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
    }

    public virtual async Task<IEnumerable<TEntity>> ObterTodosAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public virtual async Task<TEntity> ObterPorIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task AdicionarAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public virtual void Atualizar(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    public virtual void Remover(TEntity entity)
    {
        _dbSet.Remove(entity);
    }
}
