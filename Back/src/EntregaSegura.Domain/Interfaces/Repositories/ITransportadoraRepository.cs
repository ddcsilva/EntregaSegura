using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Domain.Interfaces.Repositories
{
    public interface ITransportadoraRepository : IRepository<Transportadora>
    {
        Task<Transportadora> ObterPorNomeAsync(string nome);
        Task<Transportadora> ObterTransportadoraComEntregasAsync(int id);
        Task<IEnumerable<Transportadora>> ObterTodasTransportadorasPeloNomeAsync(string nome);
    }
}