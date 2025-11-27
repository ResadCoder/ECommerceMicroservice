using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.Extensions;
using Ordering.Domain.ValueObjects;
using Shared.CQRS;

namespace Ordering.Application.Orders.Queries.GetOrderByCustomer;

public record GetOrderByCustomerHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
{
    public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery query, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
            .Include(o => o.OrderItems)
            .AsNoTracking()
            .Where(o => o.CustomerId == CustomerId.Of(query.CustomerId))
            .ToListAsync(cancellationToken);
        
        return new GetOrdersByCustomerResult(orders.ToOrders());
        
    }
}