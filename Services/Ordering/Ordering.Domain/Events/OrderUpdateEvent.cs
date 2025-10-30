using Ordering.Domain.Models;

namespace Ordering.Domain.Events;

public record OrderUpdateEvent(Order Order) : IDomainEvent;
