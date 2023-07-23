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
    private readonly IImagemService _imagemService;

    public UsuarioController(
        INotificadorErros notificadorErros,
        IUsuarioService usuarioService,
        IImagemService imagemService) : base(notificadorErros)
    {
        _usuarioService = usuarioService;
        _imagemService = imagemService;
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

        usuario.Token = _usuarioService.GerarToken(usuario);

        return Ok(new
        {
            Message = "Usuário autenticado com sucesso",
            Token = usuario.Token
        });
    }

    [HttpPost("carregar-foto/{login}")]
    public async Task<IActionResult> CarregarFoto(string login, IFormFile foto)
    {
        var usuario = await _usuarioService.ObterUsuarioPorLoginAsync(login);

        if (usuario == null) return NotFound();

        var nomeArquivo = Guid.NewGuid() + Path.GetExtension(foto.FileName);

        var caminhoFoto = await _imagemService.Carregar(foto, nomeArquivo);

        if (!await _usuarioService.AtualizarFotoUsuarioAsync(login, caminhoFoto))
        {
            return CustomResponse();
        }

        return Ok(caminhoFoto);
    }
}