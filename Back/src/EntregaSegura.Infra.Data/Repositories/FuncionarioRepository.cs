using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Interfaces;
using EntregaSegura.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EntregaSegura.Infra.Data.Repositories;

public class FuncionarioRepository : RepositoryBase<Funcionario>, IFuncionarioRepository
{
    public FuncionarioRepository(EntregaSeguraContext context) : base(context) { }

    public async Task<IEnumerable<Funcionario>> ObterTodosFuncionariosECondominiosAsync(bool rastrearAlteracoes = false)
    {
        IQueryable<Funcionario> query = _context.Funcionarios
            .Include(f => f.Pessoa)
            .Include(f => f.Condominio);

        if (!rastrearAlteracoes)
            query = query.AsNoTracking();

        return await query.ToListAsync();
    }

    public async Task<Funcionario> ObterFuncionarioPorIdECondominioAsync(int id, bool rastrearAlteracoes = false)
    {
        IQueryable<Funcionario> query = _context.Funcionarios
            .Include(f => f.Pessoa)
            .Include(f => f.Condominio)
            .Where(f => f.Id == id);

        if (!rastrearAlteracoes)
            query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync();
    }

    public async Task<Funcionario> ObterFuncionarioPorEmailAsync(string email, bool rastrearAlteracoes = false)
    {
        IQueryable<Funcionario> query = _context.Funcionarios
            .Include(f => f.Pessoa)
            .Include(f => f.Condominio)
            .Where(f => f.Pessoa.Email == email);

        if (!rastrearAlteracoes)
            query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync();
    }

    public async Task<Funcionario> ObterFuncionarioIdPorPessoaIdAsync(int pessoaId, bool rastrearAlteracoes = false)
    {
        IQueryable<Funcionario> query = _context.Funcionarios
            .Include(f => f.Pessoa)
            .Include(f => f.Condominio)
            .Where(f => f.PessoaId == pessoaId);

        if (!rastrearAlteracoes)
            query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync();
    }
}