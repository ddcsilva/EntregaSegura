using EntregaSegura.Infrastructure.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EntregaSegura.API.Extensions;

public static class IdentityExtensions
{
    public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddDbContext<EntregaSeguraIdentityContext>(options =>
        //    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        //services.AddIdentity<IdentityUser, IdentityRole>()
        //    .AddRoles<IdentityRole>()
        //    .AddEntityFrameworkStores<EntregaSeguraIdentityContext>()
        //    .AddDefaultTokenProviders();

        return services;
    }
}