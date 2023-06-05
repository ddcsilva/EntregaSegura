using System.Linq.Expressions;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Interfaces;
using EntregaSegura.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EntregaSegura.Infra.Data.Repositories;

public class UnidadeRepository : IUnidadeRepository
{
    private readonly EntregaSeguraContext _context;
    private readonly DbSet<Unidade> _dbSet;

    public UnidadeRepository(EntregaSeguraContext context)
    {
        _context = context;
        _dbSet = _context.Set<Unidade>();
    }

    public async Task<IEnumerable<Unidade>> ObterTodasUnidadesAsync()
    {
        var unidades = await _dbSet.AsNoTracking().ToListAsync();
        return unidades;
    }

    public async Task<Unidade> ObterUnidadePorIdAsync(int id)
    {
        var unidade = await _dbSet.FindAsync(id);
        return unidade;
    }

    public async Task<IEnumerable<Unidade>> BuscarAsync(Expression<Func<Unidade, bool>> predicate)
    {
        var unidades = await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
        return unidades;
    }

    public void Adicionar(Unidade unidade)
    {
        _dbSet.Add(unidade);
    }

    public void Atualizar(Unidade unidade)
    {
        _dbSet.Update(unidade);
    }

    public void Remover(Unidade unidade)
    {
        _dbSet.Remove(unidade);
    }

    public void RemoverSerie(IEnumerable<Unidade> unidades)
    {
        _dbSet.RemoveRange(unidades);
    }
}