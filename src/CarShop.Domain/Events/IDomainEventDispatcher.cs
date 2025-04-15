using CarShop.Domain.Entities;

namespace CarShop.Domain.Events;

public interface IDomainEventDispatcher
{
    Task DispatchEventsAsync(IEnumerable<AggregateRoot> entities, CancellationToken cancellationToken = default);
}