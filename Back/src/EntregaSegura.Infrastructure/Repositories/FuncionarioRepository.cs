using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Interfaces.Repositories;
using EntregaSegura.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EntregaSegura.Infrastructure.Repositories;

public class FuncionarioRepository : Repository<Funcionario>, IFuncionarioRepository
{
    public FuncionarioRepository(EntregaSeguraContext context) : base(context) { }

    public async Task<Funcionario> ObterFuncionarioComEntregasAsync(Guid funcionarioId)
    {
        var funcionario = await _context.Funcionarios
            .AsNoTracking()
            .Include(f => f.Entregas)
            .FirstOrDefaultAsync(f => f.Id == funcionarioId);

        return funcionario;
    }

    public async Task<Funcionario> ObterPorNomeAsync(string nome)
    {
        var funcionario = await _context.Funcionarios
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.Nome == nome);

        return funcionario;
    }
}