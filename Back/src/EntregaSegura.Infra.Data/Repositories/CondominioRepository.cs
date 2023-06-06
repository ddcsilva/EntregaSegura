using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Interfaces;
using EntregaSegura.Infra.Data.Contexts;

namespace EntregaSegura.Infra.Data.Repositories;

public class CondominioRepository : ICondominioRepository
{
    private readonly EntregaSeguraContext _context;
    private readonly DbSet<Condominio> _dbSet;

    public CondominioRepository(EntregaSeguraContext context)
    {
        _context = context;
        _dbSet = _context.Set<Condominio>();
    }

    public async Task<IEnumerable<Condominio>> ObterTodosCondominiosAsync()
    {
        var condominios = await _dbSet.AsNoTracking().ToListAsync();
        return condominios;
    }

    public async Task<Condominio> ObterCondominioPorIdAsync(int id)
    {
        var condominio = await _dbSet.FindAsync(id);
        return condominio;
    }

    public async Task<IEnumerable<Condominio>> BuscarAsync(Expression<Func<Condominio, bool>> predicate)
    {
        var condominios = await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
        return condominios;
    }

    public void Adicionar(Condominio condominio)
    {
        _dbSet.Add(condominio);
    }

    public void Atualizar(Condominio condominio)
    {
        _dbSet.Update(condominio);
    }

    public void Remover(Condominio condominio)
    {
        _dbSet.Remove(condominio);
    }
}