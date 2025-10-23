using Catalog.API.Models;

namespace Catalog.API.Products.GetProducts;

public record GetProductsResponse(IEnumerable<Product> Products);

public record GetProductsRequest(int? Page = 1, int? PageSize = 10);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async ([AsParameters] GetProductsRequest request,ISender sender) =>
        {
            var query = request.Adapt<GetProductsQuery>();
            
            var result = await sender.Send(query);
            
            var response = result.Adapt<GetProductsResponse>();
            
            return Results.Ok(response);
        })
        .WithName("GetProducts")
        .Produces<GetProductsResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .WithSummary("Get products")
        .WithDescription("Get products");
    }
}