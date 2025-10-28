

using Discount.gRPC;

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
    DiscountProtoService.DiscountProtoServiceClient discountProtoService
    ) 
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        await CalculateDiscount(command.Cart,cancellationToken);
        
        await basketRepository.StoreBasketAsync(command.Cart, cancellationToken);

        return new StoreBasketResult(command.Cart.UserName);
    }

    private async Task CalculateDiscount(ShoppingCart cart, CancellationToken cancellationToken)
    {
        foreach (var item in cart.Items)
        {
            var coupon = await discountProtoService.GetDiscountAsync(new GetDiscountRequest
                { ProductName = item.ProductName }, cancellationToken: cancellationToken);
            
            item.Price -= coupon.Amount;
        }
    }
}

