using System.Linq.Expressions;
using EntregaSegura.Domain.Identity;

namespace EntregaSegura.Domain.Interfaces;

public interface IUsuarioRepository : IDisposable
{
    Task<IEnumerable<User>> BuscarTodosAsync(bool rastrearAlteracoe = false);
    Task<IEnumerable<User>> BuscarPorCondicaoAsync(Expression<Func<User, bool>> expression, bool rastrearAlteracoes = false);
    Task<User> BuscarPorIdAsync(int id, bool rastrearAlteracoes = false);
    Task<User> BuscarPorLoginAsync(string login, bool rastrearAlteracoes = false);
}