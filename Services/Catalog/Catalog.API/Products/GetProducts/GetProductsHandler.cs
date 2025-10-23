using Catalog.API.Models;


namespace Catalog.API.Products.GetProducts;

public record GetProductsQuery(int? Page = 1, int? PageSize = 10) : IQuery<GetProductResult>;
public record GetProductResult(IEnumerable<Product> Products);

public class GetProductsQueryHandler(IDocumentSession session) : IQueryHandler<GetProductsQuery, GetProductResult>
{
    public async Task<GetProductResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>()
            .ToPagedListAsync(query.Page ?? 1, query.PageSize ?? 10,token: cancellationToken);
        
        return new GetProductResult(products);
    }
}