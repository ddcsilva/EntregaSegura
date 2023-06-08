using EntregaSegura.Application.DTOs;

namespace EntregaSegura.Application.Interfaces;

public interface IUnidadeService : IDisposable
{
    Task<IEnumerable<UnidadeDTO>> ObterTodasUnidadesAsync(bool incluirCondominio, bool rastrearAlteracoes);
    Task<IEnumerable<UnidadeDTO>> ObterTodasUnidadesComCondominioAsync();
    Task<UnidadeDTO> ObterUnidadePorIdAsync(int id);

    Task AdicionarAsync(UnidadeDTO unidade);
    Task<bool> AdicionarUnidadesEmMassaAsync(UnidadesEmMassaDTO unidadesDTO);
    Task AtualizarAsync(UnidadeDTO unidade);
    Task RemoverAsync(int id);

}