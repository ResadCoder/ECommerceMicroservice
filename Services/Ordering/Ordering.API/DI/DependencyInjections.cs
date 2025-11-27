using Carter;
using Shared.Exceptions.Handler;

namespace Ordering.API.DI;

public static class DependencyInjections
{
    public static IServiceCollection AddApiServices
        (this IServiceCollection services)
    {
        services.AddCarter();

        services.AddExceptionHandler<CustomExceptionHandler>();
        
        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.MapCarter();

        app.UseExceptionHandler(opts => { });
        return app;
    }
}