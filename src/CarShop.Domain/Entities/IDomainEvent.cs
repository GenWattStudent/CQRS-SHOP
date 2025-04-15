using MediatR;

namespace CarShop.Domain.Entities;

/// <summary>
/// Marker interface for domain events
/// </summary>
public interface IDomainEvent : INotification
{
    DateTime OccurredOn { get; }
}