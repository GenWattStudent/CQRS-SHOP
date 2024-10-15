using CarShop.Domain.Entities;
using CarShop.Shared.Data;
using CarShop.Shared.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarShop.Shared.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly ApplicationDbContext _context;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }


    public void Add(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
    }

    public void AddRange(IEnumerable<TEntity> entities)
    {
        _context.Set<TEntity>().AddRange(entities);
    }

    public void Delete(int id)
    {
        var entity = _context.Set<TEntity>().Find(id);

        if (entity is not null)
        {
            _context.Set<TEntity>().Remove(entity);
        }
    }

    public void Update(TEntity entity)
    {
       _context.Set<TEntity>().Update(entity);
    }

    public void Delete(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
    }
}
