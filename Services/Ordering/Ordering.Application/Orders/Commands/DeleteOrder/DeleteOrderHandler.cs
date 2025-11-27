using Ordering.Application.Data;
using Ordering.Application.Exceptions;
using Ordering.Domain.ValueObjects;
using Shared.CQRS;

namespace Ordering.Application.Orders.Commands.DeleteOrder;

public class DeleteOrderHandler(IApplicationDbContext dbContext) : ICommandHandler<DeleteOrderCommand , DeleteOrderResult>
{
    public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        
        var orderId = OrderId.Of(command.OrderId);
        
        var order = await dbContext.Orders
            .FindAsync([orderId], cancellationToken:cancellationToken)
                    ??  throw new OrderNotFoundException($"Order with Id {command.OrderId} not found.");
        
        dbContext.Orders.Remove(order);
        await dbContext.SaveChangesAsync(cancellationToken);
        
       return new DeleteOrderResult(true);
    }
}