using Ordering.Application.Data;
using Ordering.Application.DTOs;
using Ordering.Application.Exceptions;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;
using Shared.CQRS;

namespace Ordering.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderHandler(IApplicationDbContext dbContext) : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        
        var orderId = OrderId.Of(command.Order.Id);

        var order = await dbContext.Orders
            .FindAsync([orderId], cancellationToken:cancellationToken)
                    ??  throw new OrderNotFoundException($"Order with Id {command.Order.Id} not found.");
        
        
        UpdateOrderWithNewValues(order, command.Order);
        
        dbContext.Orders.Update(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateOrderResult(true);
        
    }
    private void UpdateOrderWithNewValues(Order order, OrderDto orderDto)
    {
        var shippingAddress = Address.Of(
            orderDto.ShippingAddress.FirstName,
            orderDto.ShippingAddress.LastName,
            orderDto.ShippingAddress.EmailAdress,
            orderDto.ShippingAddress.AddressLine,
            orderDto.ShippingAddress.State,
            orderDto.ShippingAddress.ZipCode,
            orderDto.ShippingAddress.Country);
        
        var billingAddress = Address.Of(
            orderDto.BillingAddress.FirstName,
            orderDto.BillingAddress.LastName,
            orderDto.BillingAddress.EmailAdress,
            orderDto.BillingAddress.AddressLine,
            orderDto.BillingAddress.Country,
            orderDto.BillingAddress.State,
            orderDto.BillingAddress.ZipCode);
        
        var payment = Payment.Of(
            orderDto.Payment.CardName,
            orderDto.Payment.CardNumber,
            orderDto.Payment.Expiration,
            orderDto.Payment.Cvv,
            orderDto.Payment.PaymentMethod
        );
        
        order.Update(
            OrderName.Of(orderDto.OrderName),
            billingAddress:billingAddress,
            shippingAddress:shippingAddress,
            payment,
            order.Status
        );
    }
}