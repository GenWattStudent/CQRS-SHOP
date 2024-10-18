using CarShop.Shared.Repositories.Interfaces;

namespace CarShop.Shared.Data;

public interface IUnitOfWork : IDisposable
{
    ICarRepository Cars { get; }
    Task<int> SaveChangesAsync();
}
