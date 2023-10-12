using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Domain.Interfaces;

public interface IFuncionarioRepository : IRepositoryBase<Funcionario>
{
    Task<IEnumerable<Funcionario>> ObterTodosFuncionariosECondominiosAsync(bool rastrearAlteracoes = false);
    Task<Funcionario> ObterFuncionarioPorIdECondominioAsync(int id, bool rastrearAlteracoes = false);
    Task<Funcionario> ObterFuncionarioPorEmailAsync(string email, bool rastrearAlteracoes = false);
    Task<Funcionario> ObterFuncionarioIdPorPessoaIdAsync(int pessoaId, bool rastrearAlteracoes = false);
}