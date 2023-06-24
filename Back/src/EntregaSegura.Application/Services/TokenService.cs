using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using EntregaSegura.Application.DTOs;
using EntregaSegura.Application.Interfaces;
using EntregaSegura.Domain.Identity;
using EntregaSegura.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EntregaSegura.Application.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly SymmetricSecurityKey _key;
    private readonly IMoradorRepository _moradorRepository;
    private readonly IFuncionarioRepository _funcionarioRepository;

    public TokenService(IConfiguration configuration, UserManager<User> userManager, IMapper mapper, IMoradorRepository moradorRepository, IFuncionarioRepository funcionarioRepository)
    {
        _configuration = configuration;
        _userManager = userManager;
        _mapper = mapper;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        _moradorRepository = moradorRepository;
        _funcionarioRepository = funcionarioRepository;
    }

    public async Task<string> GerarToken(UsuarioDTO usuarioDTO)
    {
        var usuario = _mapper.Map<User>(usuarioDTO);
        var morador = (await _moradorRepository.BuscarPorCondicaoAsync(m => m.UserId == usuario.Id)).FirstOrDefault();
        var funcionario = (await _funcionarioRepository.BuscarPorCondicaoAsync(f => f.UserId == usuario.Id)).FirstOrDefault();

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, usuarioDTO.Id.ToString()),
            new Claim(ClaimTypes.Name, usuarioDTO.UserName),
            new Claim(ClaimTypes.Email, usuarioDTO.Email)
        };

        if (morador != null)
        {
            claims.Add(new Claim("morador", morador.Id.ToString()));
        }

        if (funcionario != null)
        {
            claims.Add(new Claim("funcionario", funcionario.Id.ToString()));
        }

        var roles = await _userManager.GetRolesAsync(usuario);
        var credenciais = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var descricaoToken = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = credenciais
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(descricaoToken);

        return tokenHandler.WriteToken(token);
    }
}