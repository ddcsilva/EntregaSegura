using System.Linq.Expressions;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Interfaces;
using EntregaSegura.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EntregaSegura.Infra.Data.Repositories;

public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
{
    protected EntregaSeguraContext _context;
    protected DbSet<TEntity> _dbSet;

    public RepositoryBase(EntregaSeguraContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> BuscarTodos(bool rastrearAlteracoes = false)
    {
        return !rastrearAlteracoes
            ? await _dbSet.AsNoTracking().ToListAsync()
            : await _dbSet.ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> BuscarPorCondicao(Expression<Func<TEntity, bool>> expression, bool rastrearAlteracoes = false)
    {
        return !rastrearAlteracoes
            ? await _dbSet.AsNoTracking().Where(expression).ToListAsync()
            : await _dbSet.Where(expression).ToListAsync();
    }

    public async Task<TEntity> BuscarPorIdAsync(int id, bool rastrearAlteracoes = false)
    {
        return !rastrearAlteracoes
            ? await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id)
            : await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
    }

    public void Adicionar(TEntity entity)
    {
        _context.Add(entity);
    }

    public void Atualizar(TEntity entity)
    {
        _context.Update(entity);
    }

    public void Remover(TEntity entity)
    {
        _context.Remove(entity);
    }

    public async Task<bool> SalvarAlteracoesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public void Dispose()
    {
        _context?.Dispose();
    }
}