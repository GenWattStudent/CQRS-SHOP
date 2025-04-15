using CarShop.Domain.Entities;

namespace CarShop.Domain.Events;

public interface IDomainEventPublisher
{
    Task PublishAsync<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent;
}