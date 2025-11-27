using Basket.API.DTOs;
using Messaging.Events;

namespace Basket.API.Basket.CheckoutBasket;

public record CheckoutBasketRequest(BasketCheckoutDto BasketCheckoutDto);

public record CheckoutBasketResponse(bool IsSuccess);


public class CheckoutBasketEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {

        app.MapPost("/basket/checkout", async (CheckoutBasketRequest request, ISender sender) =>
        {

            var command = request.Adapt<CheckoutBasketCommand>();
            
            var result =  await sender.Send(command);
            
            var response = result.Adapt<CheckoutBasketResponse>();

        })
        .WithName("CheckoutBasket")
        .Produces<CheckoutBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithDescription("Checks out the basket and creates an order.");
        
            // Implementation goes here
            
            throw new NotImplementedException(); 
        
        throw new NotImplementedException();
    }
}