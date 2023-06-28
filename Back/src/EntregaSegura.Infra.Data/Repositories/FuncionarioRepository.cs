using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Interfaces;
using EntregaSegura.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EntregaSegura.Infra.Data.Repositories;

public class FuncionarioRepository : RepositoryBase<Funcionario>, IFuncionarioRepository
{
    public FuncionarioRepository(EntregaSeguraContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Funcionario>> ObterTodosFuncionariosECondominiosAsync(bool rastrearAlteracoes = false)
    {
        IQueryable<Funcionario> query = _context.Funcionarios
            .Include(f => f.Condominio);

        if (!rastrearAlteracoes)
            query = query.AsNoTracking();

        return await query.ToListAsync();
    }
}