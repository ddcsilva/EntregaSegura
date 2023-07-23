using EntregaSegura.Application.DTOs;

namespace EntregaSegura.Application.Interfaces;

public interface IUsuarioService : IDisposable
{
    Task<UsuarioDTO> ObterUsuarioPorLoginAsync(string login, bool rastrearAlteracoes = false);
    Task<UsuarioDTO> CriarContaUsuarioAsync(UsuarioDTO usuarioDTO);
    Task<bool> AtualizarFotoUsuarioAsync(string login, string caminhoFoto);

    public string GerarToken(UsuarioDTO usuarioDTO);
}