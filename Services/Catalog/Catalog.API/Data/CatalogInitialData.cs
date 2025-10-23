using Catalog.API.Models;
using Marten.Schema;

namespace Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        await using var session = store.LightweightSession();

        if (await session.Query<Product>().AnyAsync(token: cancellation))
        {
            return;
        }
        session.Store<Product>(GetPreconfiguredProducts());
        await session.SaveChangesAsync(cancellation);
    }

    private IEnumerable<Product> GetPreconfiguredProducts() => new List<Product>
    {
        new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Milk",
            Description = "This is milk description.",
            ImageUrl = "milk.jpg",
            Price = 2,
            Categories = ["Milk related"]
        },
        new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Smart phone",
            Description = "This is smart phone description.",
            ImageUrl = "smartphone.jpg",
            Price = 1700,
            Categories = ["Technology  related"]
        },
        new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Bread",
            Description = "This is bread description.",
            ImageUrl = "bread.jpg",
            Price = 1,
            Categories = ["Pastries related"]
        },
        new Product()
        {
            Id = Guid.NewGuid(),
            Name = "Car",
            Description = "This is car description.",
            ImageUrl = "car.jpg",
            Price = 2000,
            Categories = ["Car related"]
        }
    };
}
