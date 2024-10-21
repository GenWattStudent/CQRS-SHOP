using CarShop.Shared.Repositories.Interfaces;

namespace CarShop.Core.Presistence;

public interface IUnitOfWork : IDisposable
{
    ICarRepository Cars { get; }
    Task<int> SaveChangesAsync();
}
