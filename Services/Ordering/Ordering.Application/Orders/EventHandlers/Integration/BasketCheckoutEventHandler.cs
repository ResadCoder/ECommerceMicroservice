using MassTransit;
using MediatR;
using Messaging.Events;
using Microsoft.Extensions.Logging;
using Ordering.Application.DTOs;
using Ordering.Application.Orders.Commands.CreateOrder;
using Ordering.Domain.Enums;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Orders.EventHandlers;

public class BasketCheckoutEventHandler(ISender sender , ILogger<BasketCheckoutEventHandler> logger)
    : IConsumer<BasketCheckoutEvent>
{
    public async  Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        logger.LogInformation("Intergration Event: BasketCheckoutEventHandler called with BasketCheckoutEvent: {contextMessage}", context.Message);
        
        var command  = MapToCreateOrderCommand(context.Message);
        await sender.Send(command);
        
        throw new NotImplementedException();
    }

    private CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutEvent message)
    {
        var address = new AddressDto(
            message.FirstName,
            message.LastName,
            message.Email,
            message.AddressLine,
            message.State,
            message.ZipCode,
            message.Country);
        
        var orderId = Guid.NewGuid();


        var newOrder = new OrderDto(
            Id: orderId,
            CustomerId: message.CustomerId,
            OrderName: message.UserName,
            BillingAddress: address,
            ShippingAddress: address,
            Payment: new PaymentDto(
                message.CardName,
                message.CardNumber,
                message.Expiration,
                message.Cvv,
                message.PaymentMethod
            ),
            OrderStatus: OrderStatus.Pending,
            OrderItems:
            [
                new OrderItemDto(orderId, new Guid("fa572601-7da7-44a2-a22a-0402e2720467"), 2, 500),
                new OrderItemDto(orderId, new Guid("6e8ae2ad-5cd1-4361-8577-77f76926557d"), 1, 120),
            ]);
        
        return new CreateOrderCommand(newOrder);
    }
}