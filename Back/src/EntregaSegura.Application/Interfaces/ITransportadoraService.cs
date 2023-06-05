using EntregaSegura.Application.DTOs;

namespace EntregaSegura.Application.Interfaces;

public interface ITransportadoraService : IDisposable
{
    Task<IEnumerable<TransportadoraDTO>> ObterTodasTransportadorasAsync();
    Task<TransportadoraDTO> ObterTransportadoraPorIdAsync(int id);

    Task AdicionarAsync(TransportadoraDTO transportadora);
    Task AtualizarAsync(TransportadoraDTO transportadora);
    Task RemoverAsync(int id);
}