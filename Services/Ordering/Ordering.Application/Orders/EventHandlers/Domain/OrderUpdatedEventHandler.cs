using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Events;

namespace Ordering.Application.Orders.EventHandlers.Domain;

public class OrderUpdatedEventHandler(ILogger<OrderCreatedEventHandler> logger) : INotificationHandler<OrderUpdateEvent>
{
    public Task Handle(OrderUpdateEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Order Updated Domain Event: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
        
    }
}