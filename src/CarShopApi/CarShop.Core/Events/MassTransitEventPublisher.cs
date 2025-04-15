using CarShop.Domain.Entities;
using CarShop.Domain.Events;
using CarShop.Shared.Contracts;
using MassTransit;

namespace CarShop.Core.Events;

public class MassTransitEventPublisher : IDomainEventPublisher
{
    private readonly IPublishEndpoint _publishEndpoint;

    public MassTransitEventPublisher(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task PublishAsync<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent
    {
        // Convert domain events to integration events
        var message = ConvertToMessage(domainEvent);
        if (message != null)
        {
            await _publishEndpoint.Publish(message);
        }
    }

    private object? ConvertToMessage<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent
    {
        // Map domain events to integration messages
        return domainEvent switch
        {
            CarCreatedEvent e => new CarCreatedMessage(
                e.Car.Id,
                e.Car.Brand,
                e.Car.Model,
                e.Car.Year,
                e.Car.Color,
                e.Car.VIN.Value,
                e.Car.Price.Amount,
                DateTime.UtcNow),

            CarUpdatedEvent e => new CarUpdatedMessage(
                e.Car.Id,
                e.Car.Brand,
                e.Car.Model,
                e.Car.Year,
                e.Car.Color,
                e.Car.VIN.Value,
                e.Car.Price.Amount,
                DateTime.UtcNow),

            CarDeletedEvent e => new CarDeletedMessage(
                e.Id,
                DateTime.UtcNow),

            _ => null
        };
    }
}
