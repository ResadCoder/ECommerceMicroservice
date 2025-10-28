using Discount.gRPC.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC.Data;

public class DiscountContext(DbContextOptions<DiscountContext> options) : DbContext(options)
{
    public DbSet<Coupon> Coupons { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>().HasData(
            new Coupon
            {
                Id = 1,
                ProductName = "IPhone 15",
                Description = "Discount for Apple products",
                Amount = 150
            },
            new Coupon
            {
                Id = 2,
                ProductName = "Samsung S24",
                Description = "Discount for Samsung products",
                Amount = 120
            }
        );
    }
}