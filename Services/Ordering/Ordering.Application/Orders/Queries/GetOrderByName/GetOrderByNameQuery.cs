using Ordering.Application.DTOs;
using Shared.CQRS;

namespace Ordering.Application.Orders.Queries.GetOrderByName;

public record GetOrderByNameQuery(string Name) : IQuery<GetOrdersByNameResult>;

public record GetOrdersByNameResult(IEnumerable<OrderDto> Orders);




