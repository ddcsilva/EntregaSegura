using EntregaSegura.Application.DTOs;

namespace EntregaSegura.Application.Interfaces;

public interface ICondominioService : IDisposable
{
    Task<IEnumerable<CondominioDTO>> ObterTodosCondominiosAsync();
    Task<CondominioDTO> ObterCondominioPorIdAsync(int id, bool rastrearAlteracoes = false);

    Task<bool> AdicionarAsync(CondominioDTO condominioDTO);
    Task<bool> AtualizarAsync(CondominioDTO condominioDTO);
    Task<bool> RemoverAsync(int id);
}