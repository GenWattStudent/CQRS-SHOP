using CarShop.Core.Presistence.Repositories;
using CarShop.Domain.Events;
using CarShop.Shared.Data;
using CarShop.Shared.Repositories.Interfaces;

namespace CarShop.Core.Presistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private readonly IDomainEventDispatcher _domainEventDispatcher;
    private ICarRepository? _carRepository;

    public UnitOfWork(ApplicationDbContext context, IDomainEventDispatcher domainEventDispatcher)
    {
        _context = context;
        _domainEventDispatcher = domainEventDispatcher;
    }

    public ICarRepository Cars => _carRepository ??= new CarRepository(_context);

    public async Task<int> SaveChangesAsync()
    {
        var result = await _context.SaveChangesAsync();
        var entitiesWithEvents = DomainEventTracker.GetEntitiesWithEvents().ToList();

        if (entitiesWithEvents.Any())
        {
            await _domainEventDispatcher.DispatchEventsAsync(entitiesWithEvents);

            foreach (var entity in entitiesWithEvents)
            {
                entity.ClearDomainEvents();
            }
        }

        return result;
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}