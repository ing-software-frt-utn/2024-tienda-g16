using Domain.Abstractions;
using Domain.RepositoriesContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Interceptors;
using Persistence.Repositories;

namespace Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddRepositories();
        services.AddContext(configuration);
        services.AddInterceptors();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IVendedorRepository, VendedorRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPuntoDeVentaRepository, PuntoDeVentaRepository>();
        services.AddScoped<IVentaRepository, VentaRepository>();
        services.AddScoped<ISesionRepository, SesionRepository>();
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IArticuloRepository, ArticuloRepository>();
        services.AddScoped<ITiendaRepository, TiendaRepository>();
        services.AddScoped<ITipoDeComprobanteRepository, TipoDeComprobanteRepository>();
        services.AddScoped<IStockRepository, StockRepository>();
        services.AddScoped<ICondicionTributariaRepository, CondicionTributariaRepository>();

        return services;
    }

    private static IServiceCollection AddContext(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<TiendaDbContext>((serviceProvider, options) =>
        {
            var interceptor = serviceProvider.GetService<PublishDomainEventsInterceptor>();

            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"))
                .AddInterceptors(interceptor!);
        });

        return services;
    }

    private static IServiceCollection AddInterceptors(this IServiceCollection services)
    {
        services.AddScoped<PublishDomainEventsInterceptor>();

        return services;
    }
}
