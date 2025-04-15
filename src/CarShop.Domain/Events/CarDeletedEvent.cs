using CarShop.Domain.Entities;

namespace CarShop.Domain.Events;

public class CarDeletedEvent : DomainEvent
{
    public Guid Id { get; }

    public CarDeletedEvent(Guid id)
    {
        Id = id;
    }
}