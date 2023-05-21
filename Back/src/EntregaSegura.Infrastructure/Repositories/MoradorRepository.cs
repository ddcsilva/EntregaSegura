using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Interfaces.Repositories;
using EntregaSegura.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EntregaSegura.Infrastructure.Repositories;

public class MoradorRepository : Repository<Morador>, IMoradorRepository
{
    public MoradorRepository(EntregaSeguraContext context) : base(context) { }

    public async Task<Morador> ObterMoradorComEntregasAsync(int moradorId)
    {
        var morador = await _context.Moradores
            .AsNoTracking()
            .Include(m => m.Entregas)
            .FirstOrDefaultAsync(m => m.Id == moradorId);

        return morador;
    }

    public async Task<Morador> ObterMoradorComUnidadeAsync(int moradorId)
    {
        var morador = await _context.Moradores
            .AsNoTracking()
            .Include(m => m.Unidade)
            .FirstOrDefaultAsync(m => m.Id == moradorId);

        return morador;
    }

    public async Task<IEnumerable<Morador>> ObterMoradoresPorUnidadeAsync(int unidadeId)
    {
        var moradores = await _context.Moradores
            .AsNoTracking()
            .Where(m => m.UnidadeId == unidadeId)
            .ToListAsync();

        return moradores;
    }

    public async Task<Morador> ObterPorNomeAsync(string nome)
    {
        var morador = await _context.Moradores
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Nome == nome);

        return morador;
    }
}