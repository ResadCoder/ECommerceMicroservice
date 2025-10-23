namespace Basket.API.Basket.DeleteBasket;

public record DeleteBasketRequest(string UserName);

public record DeleteBasketResponse(bool IsSuccess);

public class DeleteBasketEndpoint : ICarterModule
{
    public async void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{userName}", async (string userName, ISender sender) =>
        {
            var result = await sender.Send(new DeleteBasketCommand(userName));

            var response = result.Adapt<DeleteBasketResponse>();
            
            return Results.Ok(response);
        })
        .WithName("DeleteBasketById")
        .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Delete basket by username")
        .WithDescription("Delete basket by username");
    }
}