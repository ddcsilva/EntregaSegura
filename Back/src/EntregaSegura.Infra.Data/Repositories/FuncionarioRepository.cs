using System.Linq.Expressions;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Interfaces;
using EntregaSegura.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EntregaSegura.Infra.Data.Repositories;

public class FuncionarioRepository : IFuncionarioRepository
{
    private readonly EntregaSeguraContext _context;
    private readonly DbSet<Funcionario> _dbSet;

    public FuncionarioRepository(EntregaSeguraContext context)
    {
        _context = context;
        _dbSet = _context.Set<Funcionario>();
    }

    public async Task<IEnumerable<Funcionario>> ObterTodosFuncionariosAsync()
    {
        var funcionarios = await _dbSet.AsNoTracking().ToListAsync();
        return funcionarios;
    }

    public async Task<Funcionario> ObterFuncionarioPorIdAsync(int id)
    {
        var funcionario = await _dbSet.FindAsync(id);
        return funcionario;
    }

    public async Task<IEnumerable<Funcionario>> BuscarAsync(Expression<Func<Funcionario, bool>> predicate)
    {
        var funcionarios = await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
        return funcionarios;
    }

    public void Adicionar(Funcionario funcionario)
    {
        _dbSet.Add(funcionario);
    }

    public void Atualizar(Funcionario funcionario)
    {
        _dbSet.Update(funcionario);
    }

    public void Remover(Funcionario funcionario)
    {
        _dbSet.Remove(funcionario);
    }
}