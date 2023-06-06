using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Interfaces;
using EntregaSegura.Infra.Data.Contexts;

namespace EntregaSegura.Infra.Data.Repositories;

public class EntregaRepository : IEntregaRepository
{
    private readonly EntregaSeguraContext _context;
    private readonly DbSet<Entrega> _dbSet;

    public EntregaRepository(EntregaSeguraContext context)
    {
        _context = context;
        _dbSet = _context.Set<Entrega>();
    }

    public async Task<IEnumerable<Entrega>> ObterTodasEntregasAsync()
    {
        var entregas = await _dbSet.AsNoTracking().ToListAsync();
        return entregas;
    }

    public async Task<Entrega> ObterEntregaPorIdAsync(int id)
    {
        var entrega = await _dbSet.FindAsync(id);
        return entrega;
    }

    public async Task<IEnumerable<Entrega>> BuscarAsync(Expression<Func<Entrega, bool>> predicate)
    {
        var entregas = await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
        return entregas;
    }

    public void Adicionar(Entrega entrega)
    {
        _dbSet.Add(entrega);
    }

    public void Atualizar(Entrega entrega)
    {
        _dbSet.Update(entrega);
    }

    public void Remover(Entrega entrega)
    {
        _dbSet.Remove(entrega);
    }
}