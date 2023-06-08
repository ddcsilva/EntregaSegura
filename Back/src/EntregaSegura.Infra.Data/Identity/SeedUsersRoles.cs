using EntregaSegura.Domain.Interfaces.Account;
using Microsoft.AspNetCore.Identity;

namespace EntregaSegura.Infra.Data.Identity;

public class SeedUsersRoles : ISeedUsersRoles
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public SeedUsersRoles(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public void SeedUsers()
    {
        if (_userManager.FindByEmailAsync("admin@localhost").Result == null)
        {
            ApplicationUser usuario = new ApplicationUser();
            usuario.UserName = "admin@localhost";
            usuario.Email = "admin@localhost";
            usuario.NormalizedUserName = "ADMIN@LOCALHOST";
            usuario.NormalizedEmail = "ADMIN@LOCALHOST";
            usuario.EmailConfirmed = true;
            usuario.LockoutEnabled = false;
            usuario.SecurityStamp = Guid.NewGuid().ToString();

            IdentityResult result = _userManager.CreateAsync(usuario, "Admin@123").Result;

            if (result.Succeeded)
            {
                _userManager.AddToRoleAsync(usuario, "Admin").Wait();
            }
        }
    }

    public void SeedRoles()
    {
        if (!_roleManager.RoleExistsAsync("Morador").Result)
        {
            IdentityRole role = new IdentityRole();
            role.Name = "Morador";
            role.NormalizedName = "MORADOR";
            IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
        }

        if (!_roleManager.RoleExistsAsync("Funcionario").Result)
        {
            IdentityRole role = new IdentityRole();
            role.Name = "Funcionario";
            role.NormalizedName = "FUNCIONARIO";
            IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
        }

        if (!_roleManager.RoleExistsAsync("Sindico").Result)
        {
            IdentityRole role = new IdentityRole();
            role.Name = "Sindico";
            role.NormalizedName = "SINDICO";
            IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
        }

        if (!_roleManager.RoleExistsAsync("Admin").Result)
        {
            IdentityRole role = new IdentityRole();
            role.Name = "Admin";
            role.NormalizedName = "ADMIN";
            IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
        }
    }
}