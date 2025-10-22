namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductResponse(bool IsDeleted = true);

public record DeleteProductRequest(Guid Id);

public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id}", async (Guid Id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteProductCommand(Id));
            
            var response = result.Adapt<DeleteProductResponse>();
            
            return Results.Ok(response);
            
        })
        .WithName("DeleteProduct")
        .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .WithSummary("Delete product")
        .WithDescription("Delete product");
    }
}