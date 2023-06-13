using AutoMapper;
using EntregaSegura.Application.DTOs;
using EntregaSegura.Application.Interfaces;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Identity;
using EntregaSegura.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EntregaSegura.Application.Services;

public class UsuarioService : BaseService, IUsuarioService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IMapper _mapper;
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioService(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IMapper mapper,
        IUsuarioRepository usuarioRepository,
        INotificadorErros notificadorErros) : base(notificadorErros)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<SignInResult> VerificarCredenciaisAsync(UsuarioDTO usuarioDTO, string senha)
    {
        var usuario = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == usuarioDTO.UserName);

        if (usuario == null)
        {
            Notificar("Usuário não encontrado.");
            return SignInResult.Failed;
        }

        var resultado = await _signInManager.CheckPasswordSignInAsync(usuario, senha, false);

        if (!resultado.Succeeded)
        {
            Notificar("Usuário ou senha incorretos.");
            return SignInResult.Failed;
        }

        return resultado;
    }

    public async Task<UsuarioDTO> CriarContaUsuarioAsync(UsuarioDTO usuarioDTO, string role)
    {
        var usuario = _mapper.Map<User>(usuarioDTO);

        var resultado = await _userManager.CreateAsync(usuario, usuarioDTO.Senha);

        if (!resultado.Succeeded)
        {
            Notificar("Ocorreu um erro ao criar a conta do usuário.");
            return null;
        }

        var roleUsuario = await _userManager.AddToRoleAsync(usuario, role);

        if (!roleUsuario.Succeeded)
        {
            Notificar($"Ocorreu um erro ao adicionar a role '{role}' ao usuário.");
            return null;
        }

        return _mapper.Map<UsuarioDTO>(usuario);
    }

    public async Task<UsuarioDTO> AtualizarContaUsuarioAsync(UsuarioDTO usuarioDTO)
    {
        var usuario = await _usuarioRepository.BuscarPorLoginAsync(usuarioDTO.UserName);

        if (usuario == null)
        {
            Notificar("Usuário não encontrado.");
            return null;
        }

        _mapper.Map(usuarioDTO, usuario);

        var resultado = await _userManager.UpdateAsync(usuario);

        if (!resultado.Succeeded)
        {
            Notificar("Ocorreu um erro ao atualizar a conta do usuário.");
            return null;
        }

        return _mapper.Map<UsuarioDTO>(usuario);
    }

    public async Task<UsuarioDTO> ObterUsuarioPeloLoginAsync(string login)
    {
        var usuario = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == login);

        if (usuario == null)
        {
            Notificar("Usuário não encontrado.");
            return null;
        }

        return _mapper.Map<UsuarioDTO>(usuario);
    }

    public async Task<bool> VerificarSeUsuarioExisteAsync(string login)
    {
        var usuarioExistente = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == login);

        if (usuarioExistente != null)
        {
            Notificar("Já existe um usuário com este login.");
            return true;
        }

        return false;
    }

    public void Dispose()
    {
        _userManager?.Dispose();
    }
}