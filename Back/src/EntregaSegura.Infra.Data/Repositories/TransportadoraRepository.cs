using System.Linq.Expressions;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Interfaces;
using EntregaSegura.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EntregaSegura.Infra.Data.Repositories;

public class TransportadoraRepository : ITransportadoraRepository
{
    private readonly EntregaSeguraContext _context;
    private readonly DbSet<Transportadora> _dbSet;

    public TransportadoraRepository(EntregaSeguraContext context)
    {
        _context = context;
        _dbSet = _context.Set<Transportadora>();
    }

    public async Task<IEnumerable<Transportadora>> ObterTodasTransportadorasAsync()
    {
        var transportadoras = await _dbSet.AsNoTracking().ToListAsync();
        return transportadoras;
    }

    public async Task<Transportadora> ObterTransportadoraPorIdAsync(int id)
    {
        var transportadora = await _dbSet.FindAsync(id);
        return transportadora;
    }

    public async Task<IEnumerable<Transportadora>> BuscarAsync(Expression<Func<Transportadora, bool>> predicate)
    {
        var transportadoras = await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
        return transportadoras;
    }

    public void Adicionar(Transportadora transportadora)
    {
        _dbSet.Add(transportadora);
    }

    public void Atualizar(Transportadora transportadora)
    {
        _dbSet.Update(transportadora);
    }

    public void Remover(Transportadora transportadora)
    {
        _dbSet.Remove(transportadora);
    }
}