using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Domain.Enums;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Context;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options), IApplicationDbContext
{
    public DbSet<Order> Orders  => Set<Order>();
    
    public DbSet<OrderItem> OrderItems  => Set<OrderItem>();
    
    public DbSet<Customer> Customers  => Set<Customer>();
    
    public DbSet<Product> Products => Set<Product>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}