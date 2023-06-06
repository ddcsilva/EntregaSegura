using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Interfaces;
using EntregaSegura.Infra.Data.Contexts;

namespace EntregaSegura.Infra.Data.Repositories;

public class MoradorRepository : IMoradorRepository
{
    private readonly EntregaSeguraContext _context;
    private readonly DbSet<Morador> _dbSet;

    public MoradorRepository(EntregaSeguraContext context)
    {
        _context = context;
        _dbSet = _context.Set<Morador>();
    }

    public async Task<IEnumerable<Morador>> ObterTodosMoradoresAsync()
    {
        var moradores = await _dbSet.AsNoTracking().ToListAsync();
        return moradores;
    }

    public async Task<Morador> ObterMoradorPorIdAsync(int id)
    {
        var morador = await _dbSet.FindAsync(id);
        return morador;
    }

    public async Task<IEnumerable<Morador>> BuscarAsync(Expression<Func<Morador, bool>> predicate)
    {
        var moradores = await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
        return moradores;
    }

    public void Adicionar(Morador morador)
    {
        _dbSet.Add(morador);
    }

    public void Atualizar(Morador morador)
    {
        _dbSet.Update(morador);
    }

    public void Remover(Morador morador)
    {
        _dbSet.Remove(morador);
    }
}