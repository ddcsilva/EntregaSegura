using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Interfaces.Repositories;
using EntregaSegura.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EntregaSegura.Infrastructure.Repositories;

public class CondominioRepository : Repository<Condominio>, ICondominioRepository
{
    public CondominioRepository(EntregaSeguraContext context) : base(context) { }

    public async Task<Condominio> ObterCondominioComFuncionariosAsync(int condominioId)
    {
        var condominio = await _context.Condominios
            .AsNoTracking()
            .Include(c => c.Funcionarios)
            .FirstOrDefaultAsync(c => c.Id == condominioId);

        return condominio;
    }

    public async Task<Condominio> ObterCondominioComUnidadesAsync(int condominioId)
    {
        var condominio = await _context.Condominios
            .AsNoTracking()
            .Include(c => c.Unidades)
            .FirstOrDefaultAsync(c => c.Id == condominioId);

        return condominio;
    }

    public async Task<IEnumerable<Condominio>> ObterTodosCondominiosPeloNomeAsync(string nome)
    {
        nome = nome ?? string.Empty;

        var condominios = await _context.Condominios
            .AsNoTracking()
            .Where(c => c.Nome.ToLower().Contains(nome.ToLower()))
            .ToListAsync();

        return condominios;
    }

    public async Task<Condominio> ObterCondominioComUnidadesEFuncionariosAsync(int condominioId)
    {
        var condominio = await _context.Condominios
            .AsNoTracking()
            .Include(c => c.Unidades)
            .Include(c => c.Funcionarios)
            .FirstOrDefaultAsync(c => c.Id == condominioId);

        return condominio;
    }
}