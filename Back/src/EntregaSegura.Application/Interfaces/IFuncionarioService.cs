using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Application.Interfaces;

public interface IFuncionarioService : IDisposable
{
    Task<Funcionario> Adicionar(Funcionario funcionario);
    Task<Funcionario> Atualizar(Funcionario funcionario);
    Task<bool> Remover(Guid id);
    Task<IEnumerable<Funcionario>> ObterTodosAsync();
    Task<Funcionario> ObterPorIdAsync(Guid id);
    Task<Funcionario> ObterPorNomeAsync(string nome);
    Task<Funcionario> ObterFuncionarioComEntregasAsync(Guid funcionarioId);
}