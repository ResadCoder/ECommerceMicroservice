using Carter;
using Mapster;
using MediatR;
using Ordering.Application.DTOs;
using Ordering.Application.Orders.Queries.GetOrderByCustomer;

namespace Ordering.API.Endpoints;

public record GetOrdersByCustomerResponse(IEnumerable<OrderDto> Orders);

public class GetOrderByCustomer : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/customer/{customerId}", async (Guid customerId, ISender sender) =>
        {
            var result =  await sender.Send(new GetOrdersByCustomerQuery(customerId));

            var response = result.Adapt<GetOrdersByCustomerResponse>();
            
            return Results.Ok(response);
        })
        .WithName("GetOrdersByCustomer")
        .Produces<GetOrdersByCustomerResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithDescription("Gets orders by customer ID.");
    }
}