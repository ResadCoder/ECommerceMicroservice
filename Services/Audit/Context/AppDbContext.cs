using Microsoft.EntityFrameworkCore;

namespace Audit.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Models.Audit> Audits { get; set; } = null!;
    
}