using Ordering.Application.DTOs;
using Ordering.Domain.Models;

namespace Ordering.Application.Extensions;

public static class OrderExtensions
{
    public static IEnumerable<OrderDto> ToOrders(this IEnumerable<Order> orders)
    {
        List<OrderDto> orderDtos = new List<OrderDto>();
        foreach (var order in orders)
        {
            var orderDto = new OrderDto
            (
                order.Id.Value,
                order.CustomerId.Value,
                order.OrderName.Value,
                new AddressDto(
                    order.BillingAddress.FirstName,
                    order.BillingAddress.LastName,
                    order.BillingAddress.EmailAddress,
                    order.BillingAddress.AddressLine,
                    order.BillingAddress.Country,
                    order.BillingAddress.State,
                    order.BillingAddress.ZipCode
                ),
                new AddressDto
                (
                    order.ShippingAddress.FirstName,
                    order.ShippingAddress.LastName,
                    order.ShippingAddress.EmailAddress,
                    order.ShippingAddress.AddressLine,
                    order.ShippingAddress.Country,
                    order.ShippingAddress.State,
                    order.ShippingAddress.ZipCode
                ),
                new PaymentDto
                (
                    order.Payment.CardName,
                    order.Payment.CardNumber,
                    order.Payment.CardExpiration,
                    order.Payment.Cvv,
                    order.Payment.PaymentMethod
                ),
                order.Status, 
                order.OrderItems.Select(oi => new OrderItemDto
                (
                    oi.Id.Value,
                    oi.ProductId.Value,
                    oi.Quantity,
                    oi.Price
                )).ToList()
            );
            
            orderDtos.Add(orderDto);
        }
        
        return orderDtos;
    }

    public static OrderDto ToOrderDto(this Order order)
    {
        return new OrderDto(
            order.Id.Value,
            order.CustomerId.Value,
            order.OrderName.Value,
            new AddressDto(
                order.BillingAddress.FirstName,
                order.BillingAddress.LastName,
                order.BillingAddress.EmailAddress,
                order.BillingAddress.AddressLine,
                order.BillingAddress.Country,
                order.BillingAddress.State,
                order.BillingAddress.ZipCode
            ),
            new AddressDto
            (
                order.ShippingAddress.FirstName,
                order.ShippingAddress.LastName,
                order.ShippingAddress.EmailAddress,
                order.ShippingAddress.AddressLine,
                order.ShippingAddress.Country,
                order.ShippingAddress.State,
                order.ShippingAddress.ZipCode
            ),
            new PaymentDto
            (
                order.Payment.CardName,
                order.Payment.CardNumber,
                order.Payment.CardExpiration,
                order.Payment.Cvv,
                order.Payment.PaymentMethod
            ),
            order.Status, 
            order.OrderItems.Select(oi => new OrderItemDto
            (
                oi.Id.Value,
                oi.ProductId.Value,
                oi.Quantity,
                oi.Price
            )).ToList()
        );
    }
    
}