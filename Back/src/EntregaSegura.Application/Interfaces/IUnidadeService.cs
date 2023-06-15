using EntregaSegura.Application.DTOs;

namespace EntregaSegura.Application.Interfaces;

public interface IUnidadeService : IDisposable
{
    Task<IEnumerable<UnidadeDTO>> ObterTodasUnidadesAsync();
    Task<UnidadeDTO> ObterUnidadePorIdAsync(int id, bool rastrearAlteracoes = false);

    Task<bool> AdicionarAsync(UnidadeDTO unidadeDTO);
    Task<bool> AtualizarAsync(UnidadeDTO unidadeDTO);
    Task<bool> RemoverAsync(int id);

    Task<IEnumerable<UnidadeDTO>> ObterTodasUnidadesComCondominioAsync();
    Task<bool> AdicionarUnidadesEmMassaAsync(UnidadesEmMassaDTO unidadesDTO);
}