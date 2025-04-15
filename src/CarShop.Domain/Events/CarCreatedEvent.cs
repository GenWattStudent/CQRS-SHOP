using CarShop.Domain.Entities;

namespace CarShop.Domain.Events;

/// <summary>
/// Event raised when a new car is created
/// </summary>
public class CarCreatedEvent : DomainEvent
{
    public Car Car { get; }

    public CarCreatedEvent(Car car)
    {
        Car = car;
    }
}