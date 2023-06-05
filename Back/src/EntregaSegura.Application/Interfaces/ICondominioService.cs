using EntregaSegura.Application.DTOs;

namespace EntregaSegura.Application.Interfaces;

public interface ICondominioService : IDisposable
{
    Task<IEnumerable<CondominioDTO>> ObterTodosCondominiosAsync();
    Task<CondominioDTO> ObterCondominioPorIdAsync(int id);

    Task AdicionarAsync(CondominioDTO condominio);
    Task AtualizarAsync(CondominioDTO condominio);
    Task RemoverAsync(int id);
}