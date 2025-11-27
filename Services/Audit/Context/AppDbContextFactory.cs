using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Audit.Context
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("local.settings.json", optional: false)
                .Build();

            var connectionString = config.GetConnectionString("Database");

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException("Connection string not found in local.settings.json.");

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}