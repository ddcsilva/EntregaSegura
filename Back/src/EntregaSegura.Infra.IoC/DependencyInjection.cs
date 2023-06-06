using Microsoft.Extensions.DependencyInjection;
using EntregaSegura.Application.Interfaces;
using EntregaSegura.Application.Notifications;
using EntregaSegura.Application.Services;
using EntregaSegura.Domain.Entities;
using EntregaSegura.Domain.Interfaces;
using EntregaSegura.Infra.Data.Repositories;
using EntregaSegura.Infra.Data.UnitOfWork;

namespace EntregaSegura.Infra.IoC;

public static class DependencyInjection
{
    public static IServiceCollection ResolveDependencies(this IServiceCollection services)
    {
        services.AddScoped<ICondominioRepository, CondominioRepository>();
        services.AddScoped<IEntregaRepository, EntregaRepository>();
        services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();
        services.AddScoped<IMoradorRepository, MoradorRepository>();
        services.AddScoped<ITransportadoraRepository, TransportadoraRepository>();
        services.AddScoped<IUnidadeRepository, UnidadeRepository>();

        services.AddScoped<ICondominioService, CondominioService>();
        // services.AddScoped<IEntregaService, EntregaService>();
        // services.AddScoped<IFuncionarioService, FuncionarioService>();
        // services.AddScoped<IMoradorService, MoradorService>();
        services.AddScoped<ITransportadoraService, TransportadoraService>();
        // services.AddScoped<IUnidadeService, UnidadeService>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<INotificadorErros, NotificadorErros>();

        return services;
    }
}