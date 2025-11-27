using Ordering.Application.DTOs;
using Shared.CQRS;
using Shared.Pagination;

namespace Ordering.Application.Orders.Queries.GetOrders;

public record GetOrdersQuery(PaginationRequest PaginationRequest) : IQuery<GetOrdersQueryResult>;

public record  GetOrdersQueryResult(PaginatedResult<OrderDto> Orders);