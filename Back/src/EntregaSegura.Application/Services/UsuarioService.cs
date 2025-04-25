using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using AutoMapper;
using EntregaSegura.Application.DTOs;
using EntregaSegura.Application.Interfaces;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Entities.Enums;
using EntregaSegura.Domain.Interfaces;
using EntregaSegura.Domain.Validations;
using Microsoft.IdentityModel.Tokens;

namespace EntregaSegura.Application.Services;

public class UsuarioService : BaseService, IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IMapper _mapper;

    public UsuarioService(IUsuarioRepository usuarioRepository,
                             IMapper mapper,
                             INotificadorErros notificador) : base(notificador)
    {
        _usuarioRepository = usuarioRepository;
        _mapper = mapper;
    }

    public async Task<UsuarioDTO> ObterUsuarioPorLoginAsync(string login, bool rastrearAlteracoes = false)
    {
        var usuario = await _usuarioRepository.ObterUsuarioPorLoginComDadosPessoaAsync(login, rastrearAlteracoes);
        return _mapper.Map<UsuarioDTO>(usuario);
    }

    public async Task<bool> AtualizarFotoUsuarioAsync(string login, string caminhoFoto)
    {
        var usuario = await _usuarioRepository.ObterUsuarioPorLoginComDadosPessoaAsync(login, true);

        if (usuario == null)
        {
            Notificar("Usuário não encontrado.");
            return false;
        }

        usuario.AtualizarFoto(caminhoFoto);

        _usuarioRepository.Atualizar(usuario);

        var atualizadoComSucesso = await _usuarioRepository.SalvarAlteracoesAsync();

        if (!atualizadoComSucesso)
        {
            Notificar("Ocorreu um erro ao atualizar a foto do usuário.");
            return false;
        }

        return true;
    }

    public async Task<UsuarioDTO> CriarContaUsuarioAsync(UsuarioDTO usuarioDTO)
    {
        var usuario = _mapper.Map<Usuario>(usuarioDTO);

        if (!await ValidarUsuario(usuario)) return null;

        _usuarioRepository.Adicionar(usuario);

        var adicionadoComSucesso = await _usuarioRepository.SalvarAlteracoesAsync();

        if (!adicionadoComSucesso)
        {
            Notificar("Ocorreu um erro ao adicionar o usuário.");
            return null;
        }

        usuarioDTO.Id = usuario.Id;

        return usuarioDTO;
    }

    public string GerarToken(UsuarioDTO usuarioDTO)
    {
        var perfil = "";
        switch (usuarioDTO.Perfil)
        {
            case PerfilUsuario.Administrador:
                perfil = "Administrador";
                break;
            case PerfilUsuario.Sindico:
                perfil = "Sindico";
                break;
            case PerfilUsuario.Funcionario:
                perfil = "Funcionario";
                break;
            case PerfilUsuario.Morador:
                perfil = "Morador";
                break;
        }

        var jwtTokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("ChaveSecretaParaCriacaoDoToken123456");
        var identity = new ClaimsIdentity(new Claim[]
        {
            new Claim("Id", usuarioDTO.Pessoa.Id.ToString()),
            new Claim("Nome", usuarioDTO.Pessoa.Nome),
            new Claim("Email", usuarioDTO.Pessoa.Email),
            new Claim("Perfil", perfil),
            new Claim("Foto", usuarioDTO.Foto ?? "")
        });

        var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = identity,
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = credentials
        };

        var token = jwtTokenHandler.CreateToken(tokenDescriptor);

        return jwtTokenHandler.WriteToken(token);
    }

    public void Dispose()
    {
        _usuarioRepository?.Dispose();
    }

    private async Task<bool> ValidarUsuario(Usuario usuario, bool ehAtualizacao = false)
    {
        if (!ExecutarValidacao(new UsuarioValidator(), usuario)) return false;

        if (ehAtualizacao && !VerificarForcaSenha(usuario.Senha)) return false;

        if (!string.IsNullOrWhiteSpace(usuario.Login)
            && (await _usuarioRepository.BuscarPorCondicaoAsync(u => u.Login == usuario.Login && u.Id != usuario.Id)).Any())
        {
            Notificar("Já existe um usuário com o e-mail informado.");
            return false;
        }

        return true;
    }

    private bool VerificarForcaSenha(string senha)
    {
        if (senha.Length < 8)
        {
            Notificar("A senha deve conter no mínimo 8 caracteres.");
            return false;
        }

        if (!(Regex.IsMatch(senha, "[a-z]") && Regex.IsMatch(senha, "[A-Z]") && Regex.IsMatch(senha, "[0-9]")))
        {
            Notificar("A senha deve conter letras maiúsculas, minúsculas e números.");
            return false;
        }

        if (!Regex.IsMatch(senha, "[<>@!#$%^&*()_+\\[\\]{}|',./~`\\-=]"))
        {
            Notificar("A senha deve conter pelo menos um caractere especial.");
            return false;
        }

        return true;
    }
}