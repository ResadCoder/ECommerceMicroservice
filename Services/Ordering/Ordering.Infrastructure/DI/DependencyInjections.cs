using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Data;
using Ordering.Infrastructure.Context;
using Ordering.Infrastructure.Context.Interceptors;

namespace Ordering.Infrastructure.DI;
public static class DependencyInjections
{
    public static IServiceCollection AddInfrastructureServices
        (this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DomainEventsInterceptor>();
        services.AddDbContext<ApplicationDbContext>((sp,options) =>
        {
            options.UseSqlServer(connectionString);
            options.AddInterceptors(sp.GetService<ISaveChangesInterceptor>()!);
        });
        
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        
        return services;
    }
}