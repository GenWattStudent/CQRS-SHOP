using CarShop.Domain.Entities;
using CarShop.Shared.Data;
using CarShop.Shared.Repositories;
using CarShop.Shared.Repositories.Interfaces;

namespace CarShop.Core.Presistence.Repositories;

public class CarRepository : Repository<Car>, ICarRepository
{
    public CarRepository(ApplicationDbContext context) : base(context)
    {
    }
}
