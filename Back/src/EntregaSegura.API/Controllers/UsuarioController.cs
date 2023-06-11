using System.Net;
using EntregaSegura.Application.DTOs;
using EntregaSegura.Application.Interfaces;
using EntregaSegura.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EntregaSegura.API.Controllers;

[Authorize]
[Route("api/usuarios")]
public class UsuarioController : MainController
{
    private readonly IUsuarioService _usuarioService;
    private readonly ITokenService _tokenService;
    
    public UsuarioController(IUsuarioService usuarioService,
                             ITokenService tokenService,
                             INotificadorErros notificadorErros) : base(notificadorErros)
    {
        _usuarioService = usuarioService;
        _tokenService = tokenService;
    }

    [AllowAnonymous]
    [HttpGet("ObterUsuario/{login}")]
    public async Task<ActionResult> ObterUsuario(string login)
    {
        var usuario = await _usuarioService.ObterUsuarioPeloLoginAsync(login);

        if (usuario == null)
            return NotFound();

        return CustomResponse(usuario);
    }

    [AllowAnonymous]
    [HttpPost("Registrar")]
    public async Task<ActionResult> Registrar(UsuarioDTO usuarioDTO)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState, HttpStatusCode.BadRequest);

        if (await _usuarioService.VerificarSeUsuarioExisteAsync(usuarioDTO.UserName))
        {
            NotificarErro("Já existe um usuário com este login.");
            return CustomResponse();
        }

        var usuarioRegistrado = await _usuarioService.CriarContaUsuarioAsync(usuarioDTO);

        if (usuarioRegistrado == null)
        {
            NotificarErro("Não foi possível registrar o usuário.");
            return CustomResponse();
        }

        return CustomResponse(usuarioRegistrado);
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<ActionResult> Login(LoginUsuarioDTO loginDTO)
    {
        if (!ModelState.IsValid)
        {
            return CustomResponse(ModelState);
        }

        var usuario = await _usuarioService.ObterUsuarioPeloLoginAsync(loginDTO.UserName);

        if (usuario == null)
            return CustomResponse(null, HttpStatusCode.NotFound);

        var resultadoAutenticacao =  await _usuarioService.VerificarCredenciaisAsync(usuario, loginDTO.Senha);

        if (!resultadoAutenticacao.Succeeded)
            return CustomResponse(null, HttpStatusCode.Unauthorized);

        var retorno = new {
            usuario = usuario.UserName,
            token = _tokenService.GerarToken(usuario).Result
        };

        return CustomResponse(retorno);
    }
}