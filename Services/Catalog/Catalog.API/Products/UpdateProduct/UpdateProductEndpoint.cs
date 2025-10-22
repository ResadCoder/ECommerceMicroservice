namespace Catalog.API.Products.UpdateProduct;


public record UpdateProductRequest(Guid Id, string Name,decimal Price,List<string> Categories);

public record UpdateProductResponse(bool IsSuccess);

public class UpdateProductEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products", async (UpdateProductRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateProductCommand>();
            
            var result = await sender.Send(command);
            
             var response = result.Adapt<UpdateProductResponse>();
            
            return Results.NoContent();
        })
        .WithName("UpdateProductByCategory")
        .Produces<UpdateProductResponse>(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update product ")
        .WithDescription("Update product ");
    }
}