using Application.AppServices;
using MediatR;

public class StartupServiceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly AppStartupService _startupService;

    public StartupServiceBehavior(AppStartupService startupService)
    {
        _startupService = startupService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!AppStartupService.IsUp)
        {
            _startupService.Startup();
        }

        return await next();
    }
}

