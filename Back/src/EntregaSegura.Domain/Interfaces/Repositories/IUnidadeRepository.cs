using System.Linq.Expressions;
using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Domain.Interfaces;

public interface IUnidadeRepository
{
    Task<IEnumerable<Unidade>> ObterTodasUnidadesAsync();
    Task<IEnumerable<Unidade>> ObterTodasUnidadesComCondominioAsync();
    Task<Unidade> ObterUnidadePorIdAsync(int id);
    Task<IEnumerable<Unidade>> BuscarAsync(Expression<Func<Unidade, bool>> predicate);

    void Adicionar(Unidade unidade);
    void Atualizar(Unidade unidade);
    void Remover(Unidade unidade);
    void RemoverSerie(IEnumerable<Unidade> unidade);
}