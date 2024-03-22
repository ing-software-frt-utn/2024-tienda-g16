using Microsoft.AspNetCore.Mvc.Infrastructure;
using Presentation.Common.Errors;
using Presentation.Common.Mapping;

namespace Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(
        this IServiceCollection services)
    {
        services.AddMappings();
        services.AddControllers();
        services.AddSingleton<ProblemDetailsFactory, TiendaProblemDetailsFactory>();

        return services;
    }
}

