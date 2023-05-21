using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Application.Interfaces;

public interface IFuncionarioService : IDisposable
{
    Task<Funcionario> Adicionar(Funcionario funcionario);
    Task<Funcionario> Atualizar(Funcionario funcionario);
    Task<bool> Remover(int id);
    Task<IEnumerable<Funcionario>> ObterTodosAsync();
    Task<Funcionario> ObterPorIdAsync(int id);
    Task<Funcionario> ObterPorNomeAsync(string nome);
    Task<Funcionario> ObterFuncionarioComEntregasAsync(int funcionarioId);
}