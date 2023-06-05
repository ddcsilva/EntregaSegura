using System.Linq.Expressions;
using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Domain.Interfaces;

public interface IFuncionarioRepository
{
    Task<IEnumerable<Funcionario>> ObterTodosFuncionariosAsync();
    Task<Funcionario> ObterFuncionarioPorIdAsync(int id);
    Task<IEnumerable<Funcionario>> BuscarAsync(Expression<Func<Funcionario, bool>> predicate);

    void Adicionar(Funcionario funcionario);
    void Atualizar(Funcionario funcionario);
    void Remover(Funcionario funcionario);
}