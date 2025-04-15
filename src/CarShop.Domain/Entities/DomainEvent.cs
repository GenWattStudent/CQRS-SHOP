namespace CarShop.Domain.Entities;

/// <summary>
/// Base implementation for domain events
/// </summary>
public abstract class DomainEvent : IDomainEvent
{
    /// <summary>
    /// The timestamp when this domain event occurred
    /// </summary>
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}