using Catalog.API.DTOs.Products.Create.Endpoint;
using Catalog.API.DTOs.Products.Create.Handler;

namespace Catalog.API.Products.CreateProduct;

public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateProductRequestDto request, ISender sender) =>
            {
                // CORRECT: map to command DTO, not handler
                var command = request.Adapt<CreateProductCommandDto>();
            
                var result = await sender.Send(command);
            
                var response = result.Adapt<CreateProductResponseDto>();
            
                return Results.Created($"/product/{response.Id}", response);

            })
            .WithName("CreateProduct")
            .Produces<CreateProductResponseDto>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithSummary("Create product")
            .WithDescription("Create product");
    }
}