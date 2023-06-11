using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using EntregaSegura.Application.DTOs;
using EntregaSegura.Application.Interfaces;
using EntregaSegura.Domain.Identity;
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

    public TokenService(IConfiguration configuration, UserManager<User> userManager, IMapper mapper)
    {
        _configuration = configuration;
        _userManager = userManager;
        _mapper = mapper;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
    }

    public async Task<string> GerarToken(UsuarioDTO usuarioDTO)
    {
        var usuario = _mapper.Map<User>(usuarioDTO);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, usuarioDTO.Id.ToString()),
            new Claim(ClaimTypes.Name, usuarioDTO.Login),
            new Claim(ClaimTypes.Email, usuarioDTO.Email)
        };

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