using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using EntregaSegura.API.Models;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Interfaces.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace EntregaSegura.API.Controllers;

[Route("api/token")]
public class TokenController : MainController
{
    private readonly IAutenticacaoService _autenticacaoService;
    private readonly IConfiguration _configuration;

    public TokenController(IAutenticacaoService autenticacaoService,
                           IConfiguration configuration,
                           INotificadorErros notificadorErros) : base(notificadorErros)
    {
        _autenticacaoService = autenticacaoService;
        _configuration = configuration;
    }

    [HttpPost]
    public async Task<ActionResult<TokenUsuario>> Login([FromBody] LoginModel loginModel)
    {
        var resultadoAutenticacao = await _autenticacaoService.AutenticarAsync(loginModel.Email, loginModel.Senha);

        if (resultadoAutenticacao)
        {
            return GerarToken(loginModel);
        }
        else
        {
            NotificarErro("Usuário ou senha inválidos");
            return CustomResponse(null, HttpStatusCode.BadRequest);
        }
    }

    private TokenUsuario GerarToken(LoginModel loginModel)
    {
        var claims = new[]
        {
            new Claim("email", loginModel.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var chavePrivada = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

        var credenciais = new SigningCredentials(chavePrivada, SecurityAlgorithms.HmacSha256);

        var expiracao = DateTime.UtcNow.AddMinutes(10);

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expiracao,
            signingCredentials: credenciais
        );

        return new TokenUsuario
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiracao = expiracao
        };
    }
}