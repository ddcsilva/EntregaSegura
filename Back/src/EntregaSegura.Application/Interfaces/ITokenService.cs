using EntregaSegura.Application.DTOs;

namespace EntregaSegura.Application.Interfaces;

public interface ITokenService
{
    Task<string> GerarToken(UsuarioDTO usuarioDTO);
}