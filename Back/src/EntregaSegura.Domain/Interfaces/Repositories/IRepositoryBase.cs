using System.Linq.Expressions;
using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Domain.Interfaces;

public interface IRepositoryBase<TEntity> : IDisposable where TEntity : EntityBase
{
    Task<IEnumerable<TEntity>> BuscarTodosAsync(bool rastrearAlteracoe = false);
    Task<IEnumerable<TEntity>> BuscarPorCondicaoAsync(Expression<Func<TEntity, bool>> expression, bool rastrearAlteracoes = false);
    Task<TEntity> BuscarPorIdAsync(int id, bool rastrearAlteracoes = false);

    void Adicionar(TEntity entity);
    void Atualizar(TEntity entity);
    void Remover(TEntity entity);
    
    Task<bool> SalvarAlteracoesAsync();

    Task IniciarTrasacaoAsync();
    Task SalvarTransacaoAsync();
    Task DescartarTransacaoAsync();
}