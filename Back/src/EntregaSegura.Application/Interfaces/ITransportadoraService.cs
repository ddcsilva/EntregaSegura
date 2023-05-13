using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Application.Interfaces;

public interface ITransportadoraService : IDisposable
{
    Task<Transportadora> Adicionar(Transportadora transportadora);
    Task<Transportadora> Atualizar(Transportadora transportadora);
    Task<bool> Remover(Guid id);
    Task<IEnumerable<Transportadora>> ObterTodosAsync();
    Task<Transportadora> ObterPorIdAsync(Guid id);
    Task<Transportadora> ObterPorNomeAsync(string nome);
    Task<Transportadora> ObterTransportadoraComEntregasAsync(Guid transportadoraId);
}