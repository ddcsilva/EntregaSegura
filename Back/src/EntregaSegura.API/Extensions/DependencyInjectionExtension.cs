using EntregaSegura.Application.Interfaces;
using EntregaSegura.Application.Notifications;
using EntregaSegura.Application.Services;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Interfaces.Repositories;
using EntregaSegura.Infrastructure.Contexts;
using EntregaSegura.Infrastructure.Repositories;
using EntregaSegura.Infrastructure.UnitOfWork;

namespace EntregaSegura.API.Extensions;

public static class DependencyInjectionExtension
{
    public static IServiceCollection ResolveDependencies(this IServiceCollection services)
    {
        services.AddScoped<EntregaSeguraContext>();

        services.AddScoped<ICondominioRepository, CondominioRepository>();
        services.AddScoped<IEntregaRepository, EntregaRepository>();
        services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();
        services.AddScoped<IMoradorRepository, MoradorRepository>();
        services.AddScoped<ITransportadoraRepository, TransportadoraRepository>();
        services.AddScoped<IUnidadeRepository, UnidadeRepository>();

        services.AddScoped<ICondominioService, CondominioService>();
        services.AddScoped<IEntregaService, EntregaService>();
        services.AddScoped<IFuncionarioService, FuncionarioService>();
        services.AddScoped<IMoradorService, MoradorService>();
        services.AddScoped<ITransportadoraService, TransportadoraService>();
        services.AddScoped<IUnidadeService, UnidadeService>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<INotificadorErros, NotificadorErros>();

        return services;
    }
}