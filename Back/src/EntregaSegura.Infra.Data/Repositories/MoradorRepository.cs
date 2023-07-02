using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Interfaces;
using EntregaSegura.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EntregaSegura.Infra.Data.Repositories;

public class MoradorRepository : RepositoryBase<Morador>, IMoradorRepository
{
    public MoradorRepository(EntregaSeguraContext context) : base(context) { }

    public async Task<IEnumerable<Morador>> ObterTodosMoradoresComUnidadeECondominioAsync(bool rastrearAlteracoes = false)
    {
        IQueryable<Morador> query = _context.Moradores
            .Include(m => m.Unidade)
            .ThenInclude(u => u.Condominio);

        if (!rastrearAlteracoes)
            query = query.AsNoTracking();

        return await query.ToListAsync();
    }

    public async Task<Morador> ObterMoradorPorIdComUnidadeECondominioAsync(int id, bool rastrearAlteracoes = false)
    {       
        IQueryable<Morador> query = _context.Moradores
            .Include(m => m.Unidade)
            .ThenInclude(u => u.Condominio)
            .Where(m => m.Id == id);

        if (!rastrearAlteracoes)
            query = query.AsNoTracking();
        
        return await query.FirstOrDefaultAsync();
    }
}