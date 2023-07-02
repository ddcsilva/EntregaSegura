using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Domain.Interfaces;

public interface IUsuarioRepository : IRepositoryBase<Usuario>
{
    Task<Usuario> ObterUsuarioPorLoginAsync(string login, bool rastrearAlteracoes = false);
}