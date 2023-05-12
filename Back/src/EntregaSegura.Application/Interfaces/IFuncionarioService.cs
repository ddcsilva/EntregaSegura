using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Application.Interfaces;

public interface IFuncionarioService : IDisposable
{
    Task Adicionar(Funcionario funcionario);
    Task Atualizar(Funcionario funcionario);
    Task Remover(Guid id);
    Task<IEnumerable<Funcionario>> ObterTodosAsync();
    Task<Funcionario> ObterPorIdAsync(Guid id);
    Task<Funcionario> ObterPorNomeAsync(string nome);
    Task<Funcionario> ObterFuncionarioComEntregasAsync(Guid funcionarioId);
}