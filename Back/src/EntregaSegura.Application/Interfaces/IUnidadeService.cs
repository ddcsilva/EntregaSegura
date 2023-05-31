using EntregaSegura.Application.DTOs.Unidades;
using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Application.Interfaces;

public interface IUnidadeService : IDisposable
{
    Task<Unidade> Adicionar(Unidade unidade);
    Task<bool> AdicionarUnidadesEmMassa(UnidadesEmMassaDTO unidadesDTO);
    Task<Unidade> Atualizar(Unidade unidade);
    Task<bool> Remover(int id);
    Task<IEnumerable<Unidade>> ObterTodosAsync();
    Task<Unidade> ObterPorIdAsync(int id);
    Task<IEnumerable<Unidade>> ObterUnidadesComCondominioAsync();
    Task<Unidade> ObterUnidadePorIdComCondominioEMoradoresAsync(int id);
    Task<Unidade> ObterPorUnidadePorCondominioBlocoNumeroAsync(int condominioId, string bloco, string numero);
    Task<Unidade> ObterUnidadeComMoradoresPorCondominioBlocoNumeroAsync(int condominioId, string bloco, string numero);
    Task<Unidade> ObterUnidadeComEntregasPorCondominioBlocoNumeroAsync(int condominioId, string bloco, string numero);
}