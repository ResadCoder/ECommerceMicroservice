using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.DTOs;
using Ordering.Application.Extensions;
using Shared.CQRS;
using Shared.Pagination;

namespace Ordering.Application.Orders.Queries.GetOrders;

public class GetOrdersHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersQuery, GetOrdersQueryResult>
{
    public async Task<GetOrdersQueryResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        
        var pageNumber = query.PaginationRequest.PageNumber;
        var pageSize = query.PaginationRequest.PageSize;
        
        var totalCount = await dbContext.Orders.CountAsync(cancellationToken);
        
        var orders = await dbContext.Orders
            .Include(o => o.OrderItems)
            .AsNoTracking()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new GetOrdersQueryResult(
            new PaginatedResult<OrderDto>(
                pageNumber,
                pageSize,
                totalCount, 
                orders.ToOrders())
        );

    }
}