using EntregaSegura.Application.DTOs;
using EntregaSegura.Domain.Entities.Enums;

namespace EntregaSegura.Application.Interfaces;

public interface IFuncionarioService : IDisposable
{
    Task<IEnumerable<FuncionarioDTO>> ObterTodosFuncionariosAsync();
    Task<FuncionarioDTO> ObterFuncionarioPorIdAsync(int id, bool rastrearAlteracoes = false);

    Task<bool> AdicionarAsync(FuncionarioDTO funcionario);
    Task<bool> AtualizarAsync(FuncionarioDTO funcionario);
    Task<bool> RemoverAsync(int id);

    Task<IEnumerable<FuncionarioDTO>> ObterTodosFuncionariosECondominiosAsync();
}