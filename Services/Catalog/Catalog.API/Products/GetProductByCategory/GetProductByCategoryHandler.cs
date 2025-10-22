using Catalog.API.Models;
using Catalog.API.Products.GetProducts;
using Marten;
using Shared.CQRS;

namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;

public record GetProductByCategoryResult(IEnumerable<Product> Products);

public class GetProductByCategoryHandler(IDocumentSession session, ILogger<GetProductByCategoryHandler> logger)
        : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery request, CancellationToken cancellationToken)
        {
                logger.LogInformation("Get product by id");
                
                var products  =  await session.Query<Product>()
                        .Where(p => p.Categories.Contains(request.Category))
                        .ToListAsync(cancellationToken);
                return new GetProductByCategoryResult(products);
        }
}