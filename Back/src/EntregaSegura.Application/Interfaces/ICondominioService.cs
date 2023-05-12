using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Application.Interfaces;

public interface ICondominioService : IDisposable
{
    Task Adicionar(Condominio condominio);
    Task Atualizar(Condominio condominio);
    Task Remover(Guid id);
    Task<IEnumerable<Condominio>> ObterTodosAsync();
    Task<Condominio> ObterPorIdAsync(Guid id);
    Task<Condominio> ObterPorNomeAsync(string nome);
    Task<Condominio> ObterCondominioComFuncionariosAsync(Guid condominioId);
    Task<Condominio> ObterCondominioComUnidadesAsync(Guid condominioId);
}