using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Interfaces.Repositories;
using EntregaSegura.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EntregaSegura.Infrastructure.Repositories;

public class UnidadeRepository : Repository<Unidade>, IUnidadeRepository
{
    public UnidadeRepository(EntregaSeguraContext context) : base(context) { }

    public async Task<IEnumerable<Unidade>> ObterUnidadesComCondominioAsync()
    {
        var unidades = await _context.Unidades
            .AsNoTracking()
            .Include(u => u.Condominio)
            .ToListAsync();

        return unidades;
    }

    public async Task<Unidade> ObterUnidadePorIdComCondominioEMoradoresAsync(Guid id)
    {
        var unidade = await _context.Unidades
            .AsNoTracking()
            .Include(u => u.Condominio)
            .Include(u => u.Moradores)
            .FirstOrDefaultAsync(u => u.Id == id);

        return unidade;
    }

    public async Task<Unidade> ObterPorUnidadePorCondominioBlocoNumeroAsync(Guid condominioId, string bloco, string numero)
    {
        var unidade = await _context.Unidades
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.CondominioId == condominioId && u.Bloco == bloco && u.Numero == numero);

        return unidade;
    }

    public async Task<Unidade> ObterUnidadeComEntregasPorCondominioBlocoNumeroAsync(Guid condominioId, string bloco, string numero)
    {
        var unidade = await _context.Unidades
            .AsNoTracking()
            .Include(u => u.Moradores)
            .ThenInclude(m => m.Entregas)
            .FirstOrDefaultAsync(u => u.CondominioId == condominioId && u.Bloco == bloco && u.Numero == numero);

        return unidade;
    }

    public async Task<Unidade> ObterUnidadeComMoradoresPorCondominioBlocoNumeroAsync(Guid condominioId, string bloco, string numero)
    {
        var unidade = await _context.Unidades
            .AsNoTracking()
            .Include(u => u.Moradores)
            .FirstOrDefaultAsync(u => u.CondominioId == condominioId && u.Bloco == bloco && u.Numero == numero);

        return unidade;
    }
}