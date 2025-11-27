using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class Extensions
{
    public static IServiceCollection AddMessageBroker
        (this IServiceCollection services, IConfiguration configuration, Assembly? assembly = null)
    {
        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();

            if (assembly != null)
                x.AddConsumers(assembly);

            var provider = configuration["MessageBroker:Provider"] ?? "RabbitMq";

            if (provider.Equals("Azure", StringComparison.OrdinalIgnoreCase))
            {
                
                x.UsingAzureServiceBus((context, cfg) =>
                {
                    cfg.Host(configuration["MessageBroker:AzureConnectionString"]);

                    cfg.ConfigureEndpoints(context);
                });
            }
            else
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri(configuration["MessageBroker:Host"]!), h =>
                    {
                        h.Username(configuration["MessageBroker:Username"]!);
                        h.Password(configuration["MessageBroker:Password"]!);
                    });

                    cfg.ConfigureEndpoints(context);
                });
            }
        });

        return services;
    }
}