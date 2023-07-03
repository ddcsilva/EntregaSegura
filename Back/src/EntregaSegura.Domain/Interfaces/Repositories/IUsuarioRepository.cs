using EntregaSegura.Domain.Entities;

namespace EntregaSegura.Domain.Interfaces;

public interface IUsuarioRepository : IRepositoryBase<Usuario>
{
    Task<Usuario> ObterUsuarioPorLoginComDadosPessoaAsync(string login, bool rastrearAlteracoes = false);
    Task<Usuario> ObterUsuarioPorPessoaAsync(int pessoaId, bool rastrearAlteracoes = false);
}