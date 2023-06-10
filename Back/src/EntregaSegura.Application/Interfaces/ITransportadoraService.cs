using EntregaSegura.Application.DTOs;

namespace EntregaSegura.Application.Interfaces;

public interface ITransportadoraService : IDisposable
{
    Task<IEnumerable<TransportadoraDTO>> ObterTodasTransportadorasAsync();
    Task<TransportadoraDTO> ObterTransportadoraPorIdAsync(int id);

    Task<bool> AdicionarAsync(TransportadoraDTO transportadoraDTO);
    Task<bool> AtualizarAsync(TransportadoraDTO transportadoraDTO);
    Task<bool> RemoverAsync(int id);
}