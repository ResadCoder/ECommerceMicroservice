using Basket.API.DTOs;
using MassTransit;
using Messaging.Events;

namespace Basket.API.Basket.CheckoutBasket;

public record CheckoutBasketCommand(BasketCheckoutDto Dto) : ICommand<CheckoutBasketResult>;

public record CheckoutBasketResult(bool IsSuccess);


public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketCommandValidator()
    {
        RuleFor(x => x.Dto.UserName)
            .NotEmpty().WithName("UserName");
        
        RuleFor(x => x.Dto.TotalPrice)
            .GreaterThan(0).WithName("TotalPrice");
    }
}


public class CheckoutBasketCommandHandler(IBasketRepository basketRepository , IPublishEndpoint publishEndpoint)
    : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
    public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
    {
        var basket = await basketRepository.GetBasketAsync(command.Dto.UserName, cancellationToken);
        
        if(basket == null)
            return new CheckoutBasketResult(false);
        
        var eventMessage = command.Dto.Adapt<BasketCheckoutEvent>();
        
        eventMessage.TotalPrice = basket.TotalPrice;
        
        await publishEndpoint.Publish(eventMessage, cancellationToken);
        
        await basketRepository.DeleteBasketAsync(command.Dto.UserName, cancellationToken);
        
        return new CheckoutBasketResult(true);
        
    }
}