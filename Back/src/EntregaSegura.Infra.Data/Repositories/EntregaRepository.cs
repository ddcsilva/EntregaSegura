using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Interfaces;
using EntregaSegura.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EntregaSegura.Infra.Data.Repositories;

public class EntregaRepository : RepositoryBase<Entrega>, IEntregaRepository
{
    public EntregaRepository(EntregaSeguraContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Entrega>> ObterTodasEntregasComMoradoresEFuncionariosETransportadorasAsync(bool rastrearAlteracoes = false)
    {
        IQueryable<Entrega> query = _context.Entregas
            .Include(e => e.Morador)
            .Include(e => e.Funcionario)
            .Include(e => e.Transportadora);

        if (!rastrearAlteracoes)
            query = query.AsNoTracking();

        return await query.ToListAsync();
    }
}