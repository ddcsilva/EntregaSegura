using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Interfaces;
using EntregaSegura.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EntregaSegura.Infra.Data.Repositories
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(EntregaSeguraContext context) : base(context) { }

        public async Task<Usuario> ObterUsuarioPorLoginComDadosPessoaAsync(string login, bool rastrearAlteracoes = false)
        {
            IQueryable<Usuario> query = _context.Usuarios
                .Include(u => u.Pessoa)
                .Where(u => u.Login == login);

            if (!rastrearAlteracoes)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Usuario> ObterUsuarioPorPessoaAsync(int pessoaId, bool rastrearAlteracoes = false)
        {
            IQueryable<Usuario> query = _context.Usuarios
                .Include(u => u.Pessoa)
                .Where(u => u.PessoaId == pessoaId);

            if (!rastrearAlteracoes)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync();
        }
    }
}