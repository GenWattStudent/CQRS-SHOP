using CarShop.Shared.Repositories.Interfaces;
using CarShop.Shared.Repositories;

namespace CarShop.Shared.Data;

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