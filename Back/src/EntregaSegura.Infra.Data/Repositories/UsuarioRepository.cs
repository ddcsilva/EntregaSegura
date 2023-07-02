using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Interfaces;
using EntregaSegura.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EntregaSegura.Infra.Data.Repositories
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(EntregaSeguraContext context) : base(context) { }

        public async Task<Usuario> ObterUsuarioPorLoginAsync(string login, bool rastrearAlteracoes = false)
        {
            return !rastrearAlteracoes
                ? await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Login == login)
                : await _dbSet.FirstOrDefaultAsync(x => x.Login == login);
        }
    }
}