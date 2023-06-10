using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Interfaces;
using EntregaSegura.Infra.Data.Contexts;

namespace EntregaSegura.Infra.Data.Repositories;

public class UnidadeRepository : RepositoryBase<Unidade>, IUnidadeRepository
{
    public UnidadeRepository(EntregaSeguraContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Unidade>> ObterTodasUnidadesComCondominioAsync(bool rastrearAlteracoes = false)
    {
        return !rastrearAlteracoes
            ? await _dbSet.Include(u => u.Condominio).AsNoTracking().ToListAsync()
            : await _dbSet.Include(u => u.Condominio).ToListAsync();
    }
}