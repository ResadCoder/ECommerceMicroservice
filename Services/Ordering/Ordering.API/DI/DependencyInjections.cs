namespace Ordering.API.DI;

public static class DependencyInjections
{
    public static IServiceCollection AddApiServices
        (this IServiceCollection services)
    {
        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        return app;
    }
}