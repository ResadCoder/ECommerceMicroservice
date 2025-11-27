using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.DTOs;
using Ordering.Application.Extensions;
using Ordering.Domain.Models;
using Shared.CQRS;

namespace Ordering.Application.Orders.Queries.GetOrderByName;

public class GetOrderByNameHandler(IApplicationDbContext dbbContext) : IQueryHandler<GetOrderByNameQuery, GetOrdersByNameResult>
{
    public async Task<GetOrdersByNameResult> Handle(GetOrderByNameQuery query, CancellationToken cancellationToken)
    {
        var orders = await dbbContext.Orders
            .Include(o => o.OrderItems)
            .AsNoTracking()
            .Where(o => o.OrderName.Value.Contains(query.Name))
            .ToListAsync(cancellationToken);
        
        return new GetOrdersByNameResult(orders.ToOrders());
    }
}