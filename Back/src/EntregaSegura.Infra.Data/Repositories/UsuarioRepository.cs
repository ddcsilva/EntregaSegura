using System.Linq.Expressions;
using EntregaSegura.Domain.Identity;
using EntregaSegura.Domain.Interfaces;
using EntregaSegura.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EntregaSegura.Infra.Data.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    protected EntregaSeguraContext _context;

    public UsuarioRepository(EntregaSeguraContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> BuscarTodosAsync(bool rastrearAlteracoes = false)
    {
        return !rastrearAlteracoes
            ? await _context.Users.AsNoTracking().ToListAsync()
            : await _context.Users.ToListAsync();
    }

    public async Task<IEnumerable<User>> BuscarPorCondicaoAsync(Expression<Func<User, bool>> expression, bool rastrearAlteracoes = false)
    {
        return !rastrearAlteracoes
            ? await _context.Users.AsNoTracking().Where(expression).ToListAsync()
            : await _context.Users.Where(expression).ToListAsync();
    }

    public async Task<User> BuscarPorIdAsync(int id, bool rastrearAlteracoes = false)
    {
        return !rastrearAlteracoes
            ? await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id)
            : await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<User> BuscarPorLoginAsync(string login, bool rastrearAlteracoes = false)
    {
        return !rastrearAlteracoes
            ? await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.UserName == login)
            : await _context.Users.FirstOrDefaultAsync(x => x.UserName == login);
    }

    public void Dispose()
    {
        _context?.Dispose();
    }
}