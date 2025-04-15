using CarShop.Domain.Entities;

namespace CarShop.Domain.Events;

/// <summary>
/// Interface for domain event handlers
/// </summary>
/// <typeparam name="TEvent">Type of domain event to handle</typeparam>
public interface IDomainEventHandler<in TEvent> where TEvent : IDomainEvent
{
    Task HandleAsync(TEvent domainEvent, CancellationToken cancellationToken = default);
}