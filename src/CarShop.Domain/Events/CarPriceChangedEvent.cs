using CarShop.Domain.Entities;
using CarShop.Domain.ValueObjects;

namespace CarShop.Domain.Events;

public class CarPriceChangedEvent : DomainEvent
{
    public Car Car { get; }
    public Money OldPrice { get; }
    public Money NewPrice { get; }

    public CarPriceChangedEvent(Car car, decimal oldPrice, decimal newPrice)
    {
        Car = car;
        OldPrice = Money.From(oldPrice);
        NewPrice = Money.From(newPrice);
    }
}