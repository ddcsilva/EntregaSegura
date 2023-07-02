using EntregaSegura.Application.DTOs;
using EntregaSegura.Application.Helpers;
using EntregaSegura.Application.Interfaces;
using EntregaSegura.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EntregaSegura.API.Controllers;

[Route("api/usuario")]
public class UsuarioController : MainController
{
    private readonly IUsuarioService _usuarioService;

    public UsuarioController(INotificadorErros notificadorErros, IUsuarioService usuarioService) : base(notificadorErros)
    {
        _usuarioService = usuarioService;
    }

    [AllowAnonymous]
    [HttpPost("autenticacao")]
    public async Task<ActionResult> Autenticar([FromBody] UsuarioDTO usuarioDTO)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var usuario = await _usuarioService.ObterUsuarioPorLoginAsync(usuarioDTO.Login);

        if (usuario == null)
        {
            NotificarErro("Usuário ou senha inválidos");
            return CustomResponse();
        }

        if (!Criptografia.VerificarSenha(usuarioDTO.Senha, usuario.Senha))
        {
            NotificarErro("Usuário ou senha inválidos");
            return CustomResponse();
        }

        usuario.Token = _usuarioService.GerarToken(usuarioDTO);

        return Ok(new {
            Message = "Usuário autenticado com sucesso",
            Token = usuario.Token
        });
    }
}