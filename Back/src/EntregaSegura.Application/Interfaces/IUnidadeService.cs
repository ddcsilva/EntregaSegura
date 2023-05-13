using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Application.Interfaces;

public interface IUnidadeService : IDisposable
{
    Task<Unidade> Adicionar(Unidade unidade);
    Task<Unidade> Atualizar(Unidade unidade);
    Task<bool> Remover(Guid id);
    Task<IEnumerable<Unidade>> ObterTodosAsync();
    Task<Unidade> ObterPorIdAsync(Guid id);
    Task<Unidade> ObterPorUnidadePorCondominioBlocoNumeroAsync(Guid condominioId, string bloco, string numero);
    Task<Unidade> ObterUnidadeComMoradoresPorCondominioBlocoNumeroAsync(Guid condominioId, string bloco, string numero);
    Task<Unidade> ObterUnidadeComEntregasPorCondominioBlocoNumeroAsync(Guid condominioId, string bloco, string numero);
}