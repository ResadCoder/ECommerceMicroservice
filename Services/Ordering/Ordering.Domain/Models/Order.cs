using Ordering.Domain.Events;

namespace Ordering.Domain.Models;

public class Order : Aggregate<OrderId>
{
    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
    
    public CustomerId CustomerId { get; private set; } = default!;
    
    public OrderName OrderName { get; private set; } = default!;
    
    public Address ShippingAddress { get; private set; } = default!;
    
    public Address BillingAddress { get; private set; } = default!;
    
    public Payment Payment { get; private set; } = default!;

    public OrderStatus Status { get; private set; } = OrderStatus.Pending;

    public decimal TotalPrice
    {
        get => OrderItems.Sum(x => x.Price * x.Quantity);
        
        private set { }
    }

    public static Order Create(OrderId orderId, ProductId productId,
        CustomerId customerId, OrderName orderName, Address billingAddress,
        Address shippingAddress, Payment payment
        )
    {
        var order = new Order
        {
            Id = orderId,
            CustomerId = customerId,
            OrderName = orderName,
            ShippingAddress = shippingAddress,
            BillingAddress = billingAddress,
            Payment = payment,
            Status = OrderStatus.Pending
        };
        
        order.AddDomainEvent(new OrderCreatedEvent(order));
        
        return order;
    }

    public void Update
    (
        OrderName orderName,Address billingAddress,
        Address shippingAddress, Payment payment,OrderStatus status)
    {
        OrderName = orderName;
        ShippingAddress = shippingAddress;
        Payment = payment;
        Status = status;
        BillingAddress = billingAddress;
        
        AddDomainEvent(new OrderUpdateEvent(this));
    }

    public void Add(ProductId productId, int quantity, decimal price)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
        
        var orderItem  = new OrderItem(Id, productId, price, quantity);
        _orderItems.Add(orderItem);
    }

    public void Remove(ProductId productId)
    {
          var orderItem = _orderItems.FirstOrDefault(x => x.ProductId == productId);
        _ = orderItem is not null && _orderItems.Remove(orderItem);
    }
}