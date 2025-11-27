using System.Net.Mail;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
using Shared.Behaviors;

namespace Ordering.Application.DI;

public static class DependencyInjections
{
    public static IServiceCollection AddApplicationServices
        (this IServiceCollection services,IConfiguration configuration)
    {
        services.AddMediatR(c =>
        {
            c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            c.AddOpenBehavior(typeof(ValidationBehavior<,>));
            c.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });

        services.AddFeatureManagement();
        
        services.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());
        
        
        return services;
    }
}