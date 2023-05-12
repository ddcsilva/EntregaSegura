using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Application.Interfaces;

public interface IUnidadeService : IDisposable
{
    Task Adicionar(Unidade unidade);
    Task Atualizar(Unidade unidade);
    Task Remover(Guid id);
    Task<IEnumerable<Unidade>> ObterTodosAsync();
    Task<Unidade> ObterPorIdAsync(Guid id);
    Task<Unidade> ObterPorUnidadePorCondominioBlocoNumeroAsync(Guid condominioId, string bloco, string numero);
    Task<Unidade> ObterUnidadeComMoradoresPorCondominioBlocoNumeroAsync(Guid condominioId, string bloco, string numero);
    Task<Unidade> ObterUnidadeComEntregasPorCondominioBlocoNumeroAsync(Guid condominioId, string bloco, string numero);
}