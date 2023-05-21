using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Entities.Enums;
using EntregaSegura.Domain.Interfaces.Repositories;
using EntregaSegura.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EntregaSegura.Infrastructure.Repositories;

public class EntregaRepository : Repository<Entrega>, IEntregaRepository
{
    public EntregaRepository(EntregaSeguraContext context) : base(context) { }

    public async Task<IEnumerable<Entrega>> ObterEntregasComStatusAguardandoRetiradaPorMoradorAsync(int moradorId)
    {
        var entregas = await _context.Entregas
            .AsNoTracking()
            .Include(e => e.Morador)
            .Where(e => e.Morador.Id == moradorId && e.Status == StatusEntrega.AguardandoRetirada)
            .ToListAsync();

        return entregas;
    }

    public async Task<IEnumerable<Entrega>> ObterEntregasComStatusRetiradaPorMoradorAsync(int moradorId)
    {
        var entregas = await _context.Entregas
            .AsNoTracking()
            .Include(e => e.Morador)
            .Where(e => e.Morador.Id == moradorId && e.Status == StatusEntrega.Retirada)
            .ToListAsync();

        return entregas;
    }

    public async Task<IEnumerable<Entrega>> ObterEntregasPorFuncionarioAsync(int funcionarioId)
    {
        var entregas = await _context.Entregas
            .AsNoTracking()
            .Include(e => e.Funcionario)
            .Where(e => e.Funcionario.Id == funcionarioId)
            .ToListAsync();

        return entregas;
    }

    public async Task<IEnumerable<Entrega>> ObterEntregasPorMoradorAsync(int moradorId)
    {
        var entregas = await _context.Entregas
            .AsNoTracking()
            .Include(e => e.Morador)
            .Where(e => e.Morador.Id == moradorId)
            .ToListAsync();
        
        return entregas;
    }

    public async Task<IEnumerable<Entrega>> ObterEntregasPorTransportadoraAsync(int transportadoraId)
    {
        var entregas = await _context.Entregas
            .AsNoTracking()
            .Include(e => e.Transportadora)
            .Where(e => e.Transportadora.Id == transportadoraId)
            .ToListAsync();

        return entregas;
    }

    public async Task<IEnumerable<Entrega>> ObterEntregasRecebidasPorFuncionarioAsync(int funcionarioId)
    {
        var entregas = await _context.Entregas
            .AsNoTracking()
            .Include(e => e.Funcionario)
            .Where(e => e.Funcionario.Id == funcionarioId && e.Status == StatusEntrega.Retirada)
            .ToListAsync();

        return entregas;
    }
}