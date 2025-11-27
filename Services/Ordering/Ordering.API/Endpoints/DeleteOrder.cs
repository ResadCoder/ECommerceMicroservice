using Carter;
using Mapster;
using MediatR;
using Ordering.Application.Orders.Commands.DeleteOrder;

namespace Ordering.API.Endpoints;

public record DeleteOrderResponse (bool Success);

public class DeleteOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/orders/{id}", async (Guid Id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteOrderCommand(Id));
            var response = result.Adapt<DeleteOrderResponse>();
            
            return Results.Ok(response);
        })
        .WithName("DeleteOrder")
        .Produces<DeleteOrderResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithDescription("Deletes an existing order.");
    }
}