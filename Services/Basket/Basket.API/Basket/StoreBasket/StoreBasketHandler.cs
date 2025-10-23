using Basket.API.Models;

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

public class StoreBasketHandler() 
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        var cart = command.Cart;

        return new StoreBasketResult("created");
    }
}