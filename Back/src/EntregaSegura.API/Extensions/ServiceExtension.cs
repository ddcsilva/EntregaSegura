using Microsoft.EntityFrameworkCore;
using EntregaSegura.Infra.Data.Contexts;

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
}