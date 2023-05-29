using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Application.Interfaces;

public interface ITransportadoraService : IDisposable
{
    Task<Transportadora> Adicionar(Transportadora transportadora);
    Task<Transportadora> Atualizar(Transportadora transportadora);
    Task<bool> Remover(int id);
    Task<IEnumerable<Transportadora>> ObterTodosAsync();
    Task<Transportadora> ObterPorIdAsync(int id);
    Task<Transportadora> ObterPorNomeAsync(string nome);
    Task<Transportadora> ObterTransportadoraComEntregasAsync(int transportadoraId);
    Task<IEnumerable<Transportadora>> ObterTodasTransportadorasPeloNomeAsync(string nome);
}