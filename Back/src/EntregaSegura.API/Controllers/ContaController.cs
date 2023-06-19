using System.Net;
using EntregaSegura.API.Extensions;
using EntregaSegura.Application.DTOs;
using EntregaSegura.Application.Interfaces;
using EntregaSegura.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EntregaSegura.API.Controllers;

[Authorize]
[Route("api/conta")]
public class ContaController : MainController
{
    private readonly IUsuarioService _usuarioService;
    private readonly ITokenService _tokenService;
    
    public ContaController(IUsuarioService usuarioService,
                           ITokenService tokenService,
                           INotificadorErros notificadorErros) : base(notificadorErros)
    {
        _usuarioService = usuarioService;
        _tokenService = tokenService;
    }

    [HttpGet("obter-usuario")]
    public async Task<ActionResult> ObterUsuario()
    {
        var userName = User.ObterUserName();

        var usuario = await _usuarioService.ObterUsuarioPeloLoginAsync(userName);

        if (usuario == null)
            return CustomResponse(null, HttpStatusCode.NotFound);

        return CustomResponse(usuario);
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<ActionResult> Login(LoginUsuarioDTO loginDTO)
    {
        if (!ModelState.IsValid)
        {
            return CustomResponse(ModelState, HttpStatusCode.BadRequest);
        }

        var usuario = await _usuarioService.ObterUsuarioPeloLoginAsync(loginDTO.Email);

        if (usuario == null)
            return CustomResponse(null, HttpStatusCode.NotFound);

        var resultadoAutenticacao =  await _usuarioService.VerificarCredenciaisAsync(usuario, loginDTO.Senha);

        if (!resultadoAutenticacao.Succeeded)
            return CustomResponse(null, HttpStatusCode.Unauthorized);

        var retorno = new {
            email = usuario.Email,
            token = _tokenService.GerarToken(usuario).Result
        };

        return CustomResponse(retorno);
    }
}