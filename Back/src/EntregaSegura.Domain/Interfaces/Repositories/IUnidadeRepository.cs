using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Domain.Interfaces.Repositories
{
    public interface IUnidadeRepository : IRepository<Unidade>
    {
        Task<IEnumerable<Unidade>> ObterUnidadesComCondominioAsync();
        Task<Unidade> ObterUnidadePorIdComCondominioEMoradoresAsync(int id);
        Task<Unidade> ObterPorUnidadePorCondominioBlocoNumeroAsync(int condominioId, string bloco, string numero);
        Task<Unidade> ObterUnidadeComMoradoresPorCondominioBlocoNumeroAsync(int condominioId, string bloco, string numero);
        Task<Unidade> ObterUnidadeComEntregasPorCondominioBlocoNumeroAsync(int condominioId, string bloco, string numero);
    }
}