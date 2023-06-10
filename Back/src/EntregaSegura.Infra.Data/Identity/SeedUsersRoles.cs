using EntregaSegura.Domain.Interfaces.Account;
using EntregaSegura.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace EntregaSegura.Infra.Data.Identity;

public class SeedUsersRoles : ISeedUsersRoles
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;

    public SeedUsersRoles(UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public void SeedUsers()
    {
        if (_userManager.FindByEmailAsync("admin@localhost").Result == null)
        {
            User usuario = new User();
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
            Role role = new Role();
            role.Name = "Morador";
            role.NormalizedName = "MORADOR";
            IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
        }

        if (!_roleManager.RoleExistsAsync("Funcionario").Result)
        {
            Role role = new Role();
            role.Name = "Funcionario";
            role.NormalizedName = "FUNCIONARIO";
            IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
        }

        if (!_roleManager.RoleExistsAsync("Sindico").Result)
        {
            Role role = new Role();
            role.Name = "Sindico";
            role.NormalizedName = "SINDICO";
            IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
        }

        if (!_roleManager.RoleExistsAsync("Admin").Result)
        {
            Role role = new Role();
            role.Name = "Admin";
            role.NormalizedName = "ADMIN";
            IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
        }
    }
}