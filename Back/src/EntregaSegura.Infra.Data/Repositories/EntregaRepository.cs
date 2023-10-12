using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Interfaces;
using EntregaSegura.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EntregaSegura.Infra.Data.Repositories;

public class EntregaRepository : RepositoryBase<Entrega>, IEntregaRepository
{
    public EntregaRepository(EntregaSeguraContext context) : base(context) { }

    public async Task<IEnumerable<Entrega>> ObterTodasEntregasComMoradoresEUnidadesEFuncionariosETransportadorasAsync(string emailUsuario, string perfilUsuario, bool rastrearAlteracoes = false)
    {
        IQueryable<Entrega> query = _context.Entregas
            .Include(e => e.Morador)
                .ThenInclude(m => m.Unidade)
            .Include(e => e.Morador)
                .ThenInclude(m => m.Pessoa)
            .Include(e => e.Funcionario)
                .ThenInclude(f => f.Pessoa)
            .Include(e => e.Transportadora);

        switch (perfilUsuario)
        {
            case "Administrador":
                break;
            case "Sindico":
            case "Funcionario":
                var funcionario = await ObterFuncionarioPorEmailAsync(emailUsuario);

                if (funcionario != null)
                {
                    query = query.Where(e => e.Funcionario.CondominioId == funcionario.CondominioId);
                }
                break;
            case "Morador":
                var morador = await _context.Moradores
                                            .Include(m => m.Pessoa)
                                            .Include(m => m.Unidade)
                                            .FirstOrDefaultAsync(m => m.Pessoa.Email == emailUsuario);
                if (morador != null)
                {
                    query = query.Where(e => e.Morador.UnidadeId == morador.UnidadeId);
                }
                break;
        }

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

    private async Task<Funcionario> ObterFuncionarioPorEmailAsync(string email, bool rastrearAlteracoes = false)
    {
        IQueryable<Funcionario> query = _context.Funcionarios
            .Include(f => f.Pessoa)
            .Include(f => f.Condominio)
            .Where(f => f.Pessoa.Email == email);

        if (!rastrearAlteracoes)
            query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync();
    }
}