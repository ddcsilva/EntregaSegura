using EntregaSegura.Domain.Interfaces.Account;
using Microsoft.AspNetCore.Identity;

namespace EntregaSegura.Infra.Data.Identity;

public class AutenticacaoService : IAutenticacaoService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AutenticacaoService(UserManager<ApplicationUser> userManager,
                               SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<bool> AutenticarAsync(string email, string senha)
    {
        var resultadoAutenticacao = await _signInManager.PasswordSignInAsync(email, senha, false, lockoutOnFailure: false);

        return resultadoAutenticacao.Succeeded;
    }

    public async Task<bool> RegistrarAsync(string email, string senha)
    {
        var applicationUser = new ApplicationUser { UserName = email, Email = email };

        var resultadoRegistro = await _userManager.CreateAsync(applicationUser, senha);

        if (resultadoRegistro.Succeeded)
        {
            await _signInManager.SignInAsync(applicationUser, isPersistent: false);
        }

        return resultadoRegistro.Succeeded;
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }
}