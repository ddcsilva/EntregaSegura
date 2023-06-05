using System.Linq.Expressions;
using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Domain.Interfaces;

public interface ITransportadoraRepository
{
    Task<IEnumerable<Transportadora>> ObterTodasTransportadorasAsync();
    Task<Transportadora> ObterTransportadoraPorIdAsync(int id);
    Task<IEnumerable<Transportadora>> BuscarAsync(Expression<Func<Transportadora, bool>> predicate);

    void Adicionar(Transportadora transportadora);
    void Atualizar(Transportadora transportadora);
    void Remover(Transportadora transportadora);
}
