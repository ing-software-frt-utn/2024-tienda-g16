using Application.Interfaces.Authentication;
using Application.Interfaces.Services;
using Infrastructure.Authentication;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System.Text;
using Application.Adapters;
using Infrastructure.RemoteServices.AutorizacionPagoTarjeta;
using Domain.GatewayContracts;
using Domain.RemoteServicesContracts;
using Infrastructure.RemoteServices.AutorizacionAfipService;
using AutorizacionAfipService;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddAuth(configuration);
        services.AddHttpClients(configuration);
        services.AddRemoteServices();

        return services;
    }

    public static IServiceCollection AddAuth(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        services.AddSingleton(Options.Create(jwtSettings));

        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                });

        services.AddSingleton<IPasswordHasher, PasswordHasher>();

        return services;
    }

    public static IServiceCollection AddHttpClients(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var decidirSettings = new DecidirSettings();
        configuration.Bind(DecidirSettings.SectionName, decidirSettings);
        services.AddSingleton(Options.Create(decidirSettings));

        var afipSettings = new AfipSettings();
        configuration.Bind(AfipSettings.SectionName, afipSettings);
        services.AddSingleton(Options.Create(afipSettings));

        services.AddHttpClient("Decidir", client =>
        {
            client.BaseAddress = new Uri("https://developers.decidir.com/api/v2/");
            client.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
        });

        return services;
    }

    public static IServiceCollection AddRemoteServices(
        this IServiceCollection services)
    {
        services.AddScoped<IAutorizacionPagoTarjetaAdapter, AutorizacionPagoTarjetaAdapter>();
        services.AddScoped<IAutorizacionPagoTarjetaGateway, AutorizacionPagoTarjetaGateway>();

        services.AddScoped<IAutorizacionAfipAdapter, AutorizacionAfipAdapter>();
        services.AddScoped<IAutorizacionAfipGateway, AutorizacionAfipGateway>();

        services.AddScoped<ILoginService, LoginServiceClient>();

        return services;
    }
}
