using CarShop.Domain.Entities;
using CarShop.Domain.Events;
using Microsoft.Extensions.Logging;

namespace CarShop.Core.Events;

public class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IDomainEventPublisher _publisher;
    private readonly ILogger<DomainEventDispatcher> _logger;

    public DomainEventDispatcher(IDomainEventPublisher publisher, ILogger<DomainEventDispatcher> logger)
    {
        _publisher = publisher;
        _logger = logger;
    }

    public async Task DispatchEventsAsync(IEnumerable<AggregateRoot> entities, CancellationToken cancellationToken = default)
    {
        foreach (var entity in entities)
        {
            var events = entity.DomainEvents.ToList();

            foreach (var domainEvent in events)
            {
                _logger.LogInformation("Dispatching domain event {EventName} for entity {EntityType} with ID {EntityId}",
                    domainEvent.GetType().Name,
                    entity.GetType().Name,
                    entity.Id);

                try
                {
                    await _publisher.PublishAsync(domainEvent);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error publishing domain event {EventName} for entity {EntityType} with ID {EntityId}",
                        domainEvent.GetType().Name,
                        entity.GetType().Name,
                        entity.Id);

                    throw;
                }
            }
        }
    }
}