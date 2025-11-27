using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Ordering.Application.Extensions;
using Ordering.Domain.Events;

namespace Ordering.Application.Orders.EventHandlers;

public class OrderCreatedEventHandler(
    ILogger<OrderCreatedEventHandler> logger, 
    IPublishEndpoint publisher,
    IFeatureManager featureManager
    )
    : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent domainEventHandler, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled : {DomainEvent}", domainEventHandler.GetType().Name);
        
        if (await featureManager.IsEnabledAsync("EnableOrderNotifications"))
        {
            var orderCreatedIntegrationEvent = domainEventHandler.Order.ToOrderDto();
            await publisher.Publish(orderCreatedIntegrationEvent, cancellationToken);
        }
    }
}