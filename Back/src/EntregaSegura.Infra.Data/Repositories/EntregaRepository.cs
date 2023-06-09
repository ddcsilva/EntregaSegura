using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Interfaces;
using EntregaSegura.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EntregaSegura.Infra.Data.Repositories;

public class EntregaRepository : RepositoryBase<Entrega>, IEntregaRepository
{
    public EntregaRepository(EntregaSeguraContext context) : base(context) { }

    public async Task<IEnumerable<Entrega>> ObterTodasEntregasComMoradoresEUnidadesEFuncionariosETransportadorasAsync(bool rastrearAlteracoes = false)
    {
        IQueryable<Entrega> query = _context.Entregas
            .Include(e => e.Morador)
                .ThenInclude(m => m.Unidade)
            .Include(e => e.Morador)
                .ThenInclude(m => m.Pessoa)
            .Include(e => e.Funcionario)
                .ThenInclude(f => f.Pessoa)
            .Include(e => e.Transportadora);

        if (!rastrearAlteracoes)
            query = query.AsNoTracking();

        return await query.ToListAsync();
    }

    public async Task<Entrega> ObterEntregaComMoradorEUnidadeEFuncionarioETransportadoraPorIdAsync(int id, bool rastrearAlteracoes = false)
    {
        IQueryable<Entrega> query = _context.Entregas
            .Include(e => e.Morador)
                .ThenInclude(m => m.Unidade)
            .Include(e => e.Morador)
                .ThenInclude(m => m.Pessoa)
            .Include(e => e.Funcionario)
                .ThenInclude(f => f.Pessoa)
            .Include(e => e.Transportadora)
            .Where(e => e.Id == id);

        if (!rastrearAlteracoes)
            query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync();
    }
}