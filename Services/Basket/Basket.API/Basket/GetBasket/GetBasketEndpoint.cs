

using Basket.API.Models;
using Mapster;
using MediatR;

namespace Basket.API.Basket.GetBasket;

public record GetBasketResponse(ShoppingCart Cart);

public class GetBasketEndpoint : ICarterModule
{
    public async void AddRoutes(IEndpointRouteBuilder app)
    {
       app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
       {
           var result  = await sender.Send(new GetBasketQuery(userName));

           var response = result.Adapt<GetBasketResponse>();
           
           return Results.Ok(response);
       })
       .WithName("GetBasketById")
       .Produces<GetBasketResponse>(StatusCodes.Status200OK)
       .Produces(StatusCodes.Status400BadRequest)
       .WithSummary("Get basket by username")
       .WithDescription("Get basket by username");
    }
}