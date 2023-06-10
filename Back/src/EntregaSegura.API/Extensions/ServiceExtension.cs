using Microsoft.EntityFrameworkCore;
using EntregaSegura.Infra.Data.Contexts;
using Microsoft.AspNetCore.Identity;
using EntregaSegura.Domain.Identity;

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
        services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<EntregaSeguraContext>()
            .AddDefaultTokenProviders();

        return services;
    }
}