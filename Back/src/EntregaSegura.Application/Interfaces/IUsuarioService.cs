using EntregaSegura.Application.DTOs;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Entities.Enums;

namespace EntregaSegura.Application.Interfaces;

public interface IUsuarioService : IDisposable
{
    Task<UsuarioDTO> ObterUsuarioPorLoginAsync(string login, bool rastrearAlteracoes = false);
    Task<UsuarioDTO> CriarContaUsuarioAsync(UsuarioDTO usuarioDTO, PerfilUsuario perfil);

    public string GerarToken(UsuarioDTO usuarioDTO);
}