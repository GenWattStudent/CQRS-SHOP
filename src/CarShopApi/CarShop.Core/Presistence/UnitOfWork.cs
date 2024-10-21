using CarShop.Core.Presistence.Repositories;
using CarShop.Shared.Data;
using CarShop.Shared.Repositories.Interfaces;

namespace CarShop.Core.Presistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private ICarRepository? _carRepository;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public ICarRepository Cars => _carRepository ??= new CarRepository(_context);

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}