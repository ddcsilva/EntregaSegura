using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Application.Interfaces;

public interface IMoradorService
{
    Task<Morador> Adicionar(Morador morador);
    Task<Morador> Atualizar(Morador morador);
    Task<bool> Remover(int id);
    Task<IEnumerable<Morador>> ObterTodosAsync();
    Task<Morador> ObterPorIdAsync(int id);
    Task<Morador> ObterPorNomeAsync(string nome);
    Task<IEnumerable<Morador>> ObterMoradoresPorUnidadeAsync(int unidadeId);
    Task<Morador> ObterMoradorComEntregasAsync(int moradorId);
    Task<Morador> ObterMoradorComUnidadeAsync(int moradorId);
}