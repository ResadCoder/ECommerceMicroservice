using Discount.gRPC;
using MassTransit;
using Shared.Models;

namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;

public record StoreBasketResult(string Username);

public class StoreBasketCommandHandlerValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandHandlerValidator()
    {
        RuleFor(x => x.Cart)
            .NotEmpty().WithMessage("Cart is required");
        RuleFor(x => x.Cart.UserName)
            .NotEmpty().WithMessage("Username is required");
    }
}

public class StoreBasketHandler(
    IBasketRepository basketRepository,
    ISendEndpointProvider sendEndpointProvider 
) 
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        await basketRepository.StoreBasketAsync(command.Cart, cancellationToken);

        // 👇 Send directly to queue named "audit-queue"
        var sendEndpoint = await sendEndpointProvider.GetSendEndpoint(
            new Uri("queue:audit-queue"));
        
        await sendEndpoint.Send(new AuditFunctionDto(
            "Basket.API",
            "StoreBasket",
            $"Basket stored for user {command.Cart.UserName}",
            Shared.Enums.LogStatus.Success,
            DateTime.UtcNow
        ), cancellationToken);
        
        return new StoreBasketResult(command.Cart.UserName);
    }
}


    // private async Task CalculateDiscount(ShoppingCart cart, CancellationToken cancellationToken)
    // {
    //     foreach (var item in cart.Items)
    //     {
    //         var coupon = await discountProtoService.GetDiscountAsync(new GetDiscountRequest
    //             { ProductName = item.ProductName }, cancellationToken: cancellationToken);
    //         
    //         item.Price -= coupon.Amount;
    //     }
    // }


