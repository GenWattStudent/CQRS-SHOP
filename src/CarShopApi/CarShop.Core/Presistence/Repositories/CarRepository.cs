using CarShop.Domain.Entities;
using CarShop.Shared.Data;
using CarShop.Shared.Data.Mapping;
using CarShop.Shared.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarShop.Core.Presistence.Repositories;

public class CarRepository : ICarRepository
{
    private readonly ApplicationDbContext _context;

    public CarRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Car>> GetAllAsync()
    {
        var entities = await _context.Cars.ToListAsync();
        return entities.Select(EntityMapper.ToDomain);
    }

    public async Task<Car?> GetByIdAsync(Guid id)
    {
        var entity = await _context.Cars.FindAsync(id);
        return entity != null ? EntityMapper.ToDomain(entity) : null;
    }

    public void Add(Car entity)
    {
        _context.Cars.Add(EntityMapper.ToEntity(entity));

        // Track the domain entity so we can access its events later
        DomainEventTracker.TrackEntity(entity);
    }

    public void AddRange(IEnumerable<Car> entities)
    {
        var carEntities = entities.Select(EntityMapper.ToEntity).ToList();
        _context.Cars.AddRange(carEntities);

        // Track all domain entities
        foreach (var entity in entities)
        {
            DomainEventTracker.TrackEntity(entity);
        }
    }

    public void Update(Car entity)
    {
        var existingEntity = _context.Cars.Find(entity.Id);

        if (existingEntity != null)
        {
            existingEntity.Brand = entity.Brand;
            existingEntity.Model = entity.Model;
            existingEntity.Year = entity.Year;
            existingEntity.Color = entity.Color;
            existingEntity.VIN = entity.VIN.Value;
            existingEntity.Price = entity.Price.Amount;
            existingEntity.ImageUrl = entity.ImageUrl;
            existingEntity.UpdatedAt = entity.UpdatedAt;
        }

        // Track the domain entity for event processing
        DomainEventTracker.TrackEntity(entity);
    }

    public void Delete(Guid id)
    {
        var entity = _context.Cars.Find(id);
        if (entity != null)
        {
            _context.Cars.Remove(entity);

            // Create a domain entity for event tracking
            var domainEntity = EntityMapper.ToDomain(entity);
            domainEntity.MarkAsDeleted();
            DomainEventTracker.TrackEntity(domainEntity);
        }
    }

    public void Delete(Car entity)
    {
        var carEntity = EntityMapper.ToEntity(entity);
        _context.Cars.Remove(carEntity);

        // Mark as deleted and track the domain entity for event processing
        entity.MarkAsDeleted();
        DomainEventTracker.TrackEntity(entity);
    }
}
