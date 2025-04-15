using CarShop.Domain.Entities;

namespace CarShop.Domain.Events;

/// <summary>
/// Event raised when a car is updated
/// </summary>
public class CarUpdatedEvent : DomainEvent
{
    public Car Car { get; }

    public CarUpdatedEvent(Car car)
    {
        Car = car;
    }
}