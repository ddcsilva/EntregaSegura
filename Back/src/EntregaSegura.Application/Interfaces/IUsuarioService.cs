using EntregaSegura.Application.DTOs;
using Microsoft.AspNetCore.Identity;

namespace EntregaSegura.Application.Interfaces;

public interface IUsuarioService : IDisposable
{
    Task<bool> VerificarSeUsuarioExisteAsync(string login);
    Task<UsuarioDTO> ObterUsuarioPeloLoginAsync(string login);
    
    Task<SignInResult> VerificarCredenciaisAsync(UsuarioDTO usuarioDTO, string senha);
    Task<UsuarioDTO> CriarContaUsuarioAsync(UsuarioDTO usuarioDTO);
    Task<UsuarioDTO> AtualizarContaUsuarioAsync(UsuarioDTO usuarioDTO);
}