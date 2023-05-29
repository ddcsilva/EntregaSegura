using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Application.Interfaces;

public interface ICondominioService : IDisposable
{
    Task<Condominio> Adicionar(Condominio condominio);
    Task<Condominio> Atualizar(Condominio condominio);
    Task<bool> Remover(int id);
    Task<IEnumerable<Condominio>> ObterTodosAsync();
    Task<IEnumerable<Condominio>> ObterTodosCondominiosPeloNomeAsync(string nome);
    Task<Condominio> ObterPorIdAsync(int id);
    Task<Condominio> ObterCondominioComFuncionariosAsync(int condominioId);
    Task<Condominio> ObterCondominioComUnidadesAsync(int condominioId);
    Task<Condominio> ObterCondominioComUnidadesEFuncionariosAsync(int condominioId);
}