using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ordering.Domain.Abstractions;

namespace Ordering.Infrastructure.Context.Interceptors;

public class DomainEventsInterceptor(IMediator mediator) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        DispatchDomainEventsAsync(eventData.Context).GetAwaiter().GetResult();
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        DispatchDomainEventsAsync(eventData.Context, cancellationToken).GetAwaiter().GetResult();
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private async Task DispatchDomainEventsAsync(DbContext? context, CancellationToken cancellationToken = default)
    {
         if (context is null) return;
         
         var aggregates = context.ChangeTracker
             .Entries<IAggregate>()
             .Where(a => a.Entity.DomainEvents.Any())
             .Select(a => a.Entity);
         
         var domainEvents = aggregates
             .SelectMany(a => a.DomainEvents)
             .ToList();
         
         aggregates.ToList().ForEach(a => a.ClearDomainEvents());
         
         foreach (var domainEvent in domainEvents)
             await mediator.Publish(domainEvent, cancellationToken);
    }
    
    
}