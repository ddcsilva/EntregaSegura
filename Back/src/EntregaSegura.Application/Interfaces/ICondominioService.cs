using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Application.Interfaces;

public interface ICondominioService : IDisposable
{
    Task<Condominio> Adicionar(Condominio condominio);
    Task<Condominio> Atualizar(Condominio condominio);
    Task<bool> Remover(Guid id);
    Task<IEnumerable<Condominio>> ObterTodosAsync();
    Task<Condominio> ObterPorIdAsync(Guid id);
    Task<Condominio> ObterPorNomeAsync(string nome);
    Task<Condominio> ObterCondominioComFuncionariosAsync(Guid condominioId);
    Task<Condominio> ObterCondominioComUnidadesAsync(Guid condominioId);
    Task<Condominio> ObterCondominioComUnidadesEFuncionariosAsync(Guid condominioId);
}