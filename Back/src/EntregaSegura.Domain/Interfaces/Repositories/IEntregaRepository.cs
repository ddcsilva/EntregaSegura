using System.Linq.Expressions;
using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Domain.Interfaces;

public interface IEntregaRepository
{
    Task<IEnumerable<Entrega>> ObterTodasEntregasAsync();
    Task<Entrega> ObterEntregaPorIdAsync(int id);
    Task<IEnumerable<Entrega>> BuscarAsync(Expression<Func<Entrega, bool>> predicate);

    void Adicionar(Entrega entrega);
    void Atualizar(Entrega entrega);
    void Remover(Entrega entrega);
}