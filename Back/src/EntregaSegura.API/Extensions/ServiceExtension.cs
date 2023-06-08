using Microsoft.EntityFrameworkCore;
using EntregaSegura.Infra.Data.Contexts;
using EntregaSegura.Infra.Data.Identity;
using Microsoft.AspNetCore.Identity;

namespace EntregaSegura.API.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection AddContexts(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EntregaSeguraContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(EntregaSeguraContext).Assembly.FullName)));

        return services;
    }

    public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<EntregaSeguraContext>()
            .AddDefaultTokenProviders();

        return services;
    }
}