using System.Linq.Expressions;
using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Domain.Interfaces;

public interface IMoradorRepository
{
    Task<IEnumerable<Morador>> ObterTodosMoradoresAsync();
    Task<Morador> ObterMoradorPorIdAsync(int id);
    Task<IEnumerable<Morador>> BuscarAsync(Expression<Func<Morador, bool>> predicate);

    void Adicionar(Morador morador);
    void Atualizar(Morador morador);
    void Remover(Morador morador);
}