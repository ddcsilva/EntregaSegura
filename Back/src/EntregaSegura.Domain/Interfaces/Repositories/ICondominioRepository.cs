using System.Linq.Expressions;
using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Domain.Interfaces;

public interface ICondominioRepository
{
    Task<IEnumerable<Condominio>> ObterTodosCondominiosAsync();
    Task<Condominio> ObterCondominioPorIdAsync(int id);
    Task<IEnumerable<Condominio>> BuscarAsync(Expression<Func<Condominio, bool>> predicate);

    void Adicionar(Condominio condominio);
    void Atualizar(Condominio condominio);
    void Remover(Condominio condominio);
}