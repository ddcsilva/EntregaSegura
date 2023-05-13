using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Application.Interfaces;

public interface IMoradorService
{
    Task<Morador> Adicionar(Morador morador);
    Task<Morador> Atualizar(Morador morador);
    Task<bool> Remover(Guid id);
    Task<IEnumerable<Morador>> ObterTodosAsync();
    Task<Morador> ObterPorIdAsync(Guid id);
    Task<Morador> ObterPorNomeAsync(string nome);
    Task<IEnumerable<Morador>> ObterMoradoresPorUnidadeAsync(Guid unidadeId);
    Task<Morador> ObterMoradorComEntregasAsync(Guid moradorId);
    Task<Morador> ObterMoradorComUnidadeAsync(Guid moradorId);
}