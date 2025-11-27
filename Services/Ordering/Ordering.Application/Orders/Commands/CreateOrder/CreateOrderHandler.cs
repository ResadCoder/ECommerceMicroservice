using Ordering.Application.Data;
using Ordering.Application.DTOs;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;
using Shared.CQRS;

namespace Ordering.Application.Orders.Commands.CreateOrder;

public class CreateOrderHandler(IApplicationDbContext dbContext) 
    : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = CreateNewOrder(request.Dto);
        
        dbContext.Orders.Add(order);
        
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return new CreateOrderResult(order.Id.Value);
    }


    private Order CreateNewOrder(OrderDto dto)
    {
        var shippingAddress = Address.Of(
            dto.ShippingAddress.FirstName,
            dto.ShippingAddress.LastName,
            dto.ShippingAddress.EmailAdress,
            dto.ShippingAddress.AddressLine,
            dto.ShippingAddress.State,
            dto.ShippingAddress.ZipCode,
            dto.ShippingAddress.Country);
        
        var billingAddress = Address.Of(
            dto.BillingAddress.FirstName,
            dto.BillingAddress.LastName,
            dto.BillingAddress.EmailAdress,
            dto.BillingAddress.AddressLine,
            dto.BillingAddress.State,
            dto.BillingAddress.ZipCode,
            dto.BillingAddress.Country);
        
        var newOrder = Order.Create(
           orderId: OrderId.Of(Guid.NewGuid()),
            ProductId.Of(dto.OrderItems.FirstOrDefault()!.ProductId),
           customerId: CustomerId.Of(dto.CustomerId),
           orderName: OrderName.Of(dto.OrderName),
           billingAddress: billingAddress, 
           shippingAddress: shippingAddress,
           payment: Payment.Of(
                dto.Payment.CardName,
                dto.Payment.CardNumber,
                dto.Payment.Expiration,
                dto.Payment.Cvv,
                dto.Payment.PaymentMethod
            ));


        foreach (var item in dto.OrderItems)
        {
            newOrder.Add(ProductId.Of(item.ProductId), item.Quantity , item.Price);
        }
        
        return newOrder;
    }
}