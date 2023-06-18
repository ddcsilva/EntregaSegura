using Microsoft.Extensions.DependencyInjection;
using EntregaSegura.Application.Interfaces;
using EntregaSegura.Application.Notifications;
using EntregaSegura.Application.Services;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Interfaces;
using EntregaSegura.Infra.Data.Repositories;

namespace EntregaSegura.Infra.IoC;

public static class DependencyInjection
{
    public static IServiceCollection ResolveDependencies(this IServiceCollection services)
    {
        ResolverDependenciasRepository(services);
        ResolverDependenciasServices(services);
        ResolverOutrasDependencias(services);

        return services;
    }

    private static void ResolverDependenciasRepository(this IServiceCollection services)
    {
        services.AddScoped<ICondominioRepository, CondominioRepository>();
        services.AddScoped<IEntregaRepository, EntregaRepository>();
        services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();
        services.AddScoped<IMoradorRepository, MoradorRepository>();
        services.AddScoped<ITransportadoraRepository, TransportadoraRepository>();
        services.AddScoped<IUnidadeRepository, UnidadeRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
    }

    private static void ResolverDependenciasServices(this IServiceCollection services)
    {
        services.AddScoped<ICondominioService, CondominioService>();
        services.AddScoped<IEntregaService, EntregaService>();
        services.AddScoped<IFuncionarioService, FuncionarioService>();
        services.AddScoped<IMoradorService, MoradorService>();
        services.AddScoped<ITransportadoraService, TransportadoraService>();
        services.AddScoped<IUnidadeService, UnidadeService>();
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IImagemService, ImagemService>();
        services.AddScoped<IEmailService, EmailService>();
    }

    private static void ResolverOutrasDependencias(this IServiceCollection services)
    {
        services.AddScoped<INotificadorErros, NotificadorErros>();
    }
}