using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Application.Interfaces;

public interface ITransportadoraService : IDisposable
{
    Task Adicionar(Transportadora transportadora);
    Task Atualizar(Transportadora transportadora);
    Task Remover(Guid id);
    Task<IEnumerable<Transportadora>> ObterTodosAsync();
    Task<Transportadora> ObterPorIdAsync(Guid id);
    Task<Transportadora> ObterPorNomeAsync(string nome);
    Task<Transportadora> ObterTransportadoraComEntregasAsync(Guid transportadoraId);
}