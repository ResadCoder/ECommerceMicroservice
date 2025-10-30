using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Infrastructure.Context;

namespace Ordering.Infrastructure.DI;

public static class DependencyInjections
{
    public static IServiceCollection AddInfrastructureServices
        (this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");

        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
        // services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        
        return services;
    }
    
}