using Catalog.API.Models;
using Marten;
using Shared.CQRS;

namespace Catalog.API.Products.GetProducts;

public record GetProductsQuery() : IQuery<GetProductResult>;
public record GetProductResult(IEnumerable<Product> Products);

public class GetProductsQueryHandler(IDocumentSession session) : IQueryHandler<GetProductsQuery, GetProductResult>
{
    public async Task<GetProductResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>().ToListAsync(token: cancellationToken);
        return new GetProductResult(products);
    }
}