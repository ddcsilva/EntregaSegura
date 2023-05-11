using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Entities.Enums;
using EntregaSegura.Domain.Interfaces.Repositories;
using EntregaSegura.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EntregaSegura.Infrastructure.Repositories;

public class EntregaRepository : Repository<Entrega>, IEntregaRepository
{
    public EntregaRepository(EntregaSeguraContext context) : base(context) { }

    public async Task<IEnumerable<Entrega>> ObterEntregasComStatusAguardandoRetiradaPorMoradorAsync(Guid moradorId)
    {
        var entregas = await _context.Entregas
            .AsNoTracking()
            .Include(e => e.Morador)
            .Where(e => e.Morador.Id == moradorId && e.Status == StatusEntrega.AguardandoRetirada)
            .ToListAsync();

        return entregas;
    }

    public async Task<IEnumerable<Entrega>> ObterEntregasComStatusRetiradaPorMoradorAsync(Guid moradorId)
    {
        var entregas = await _context.Entregas
            .AsNoTracking()
            .Include(e => e.Morador)
            .Where(e => e.Morador.Id == moradorId && e.Status == StatusEntrega.Retirada)
            .ToListAsync();

        return entregas;
    }

    public async Task<IEnumerable<Entrega>> ObterEntregasPorFuncionarioAsync(Guid funcionarioId)
    {
        var entregas = await _context.Entregas
            .AsNoTracking()
            .Include(e => e.Funcionario)
            .Where(e => e.Funcionario.Id == funcionarioId)
            .ToListAsync();

        return entregas;
    }

    public async Task<IEnumerable<Entrega>> ObterEntregasPorMoradorAsync(Guid moradorId)
    {
        var entregas = await _context.Entregas
            .AsNoTracking()
            .Include(e => e.Morador)
            .Where(e => e.Morador.Id == moradorId)
            .ToListAsync();
        
        return entregas;
    }

    public async Task<IEnumerable<Entrega>> ObterEntregasPorTransportadoraAsync(Guid transportadoraId)
    {
        var entregas = await _context.Entregas
            .AsNoTracking()
            .Include(e => e.Transportadora)
            .Where(e => e.Transportadora.Id == transportadoraId)
            .ToListAsync();

        return entregas;
    }

    public async Task<IEnumerable<Entrega>> ObterEntregasRecebidasPorFuncionarioAsync(Guid funcionarioId)
    {
        var entregas = await _context.Entregas
            .AsNoTracking()
            .Include(e => e.Funcionario)
            .Where(e => e.Funcionario.Id == funcionarioId && e.Status == StatusEntrega.Retirada)
            .ToListAsync();

        return entregas;
    }
}