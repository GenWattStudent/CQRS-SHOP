using System.Collections.Concurrent;
using CarShop.Domain.Entities;

namespace CarShop.Core.Presistence;

public class DomainEventTracker
{
    private static readonly ConcurrentDictionary<Guid, AggregateRoot> _trackedEntities = new();

    public static void TrackEntity(AggregateRoot entity)
    {
        _trackedEntities.AddOrUpdate(entity.Id, entity, (_, _) => entity);
    }

    public static IEnumerable<AggregateRoot> GetEntitiesWithEvents()
    {
        var entitiesWithEvents = _trackedEntities.Values
            .Where(e => e.DomainEvents.Any())
            .ToList();

        _trackedEntities.Clear();

        return entitiesWithEvents;
    }
}