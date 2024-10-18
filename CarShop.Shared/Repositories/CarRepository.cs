using CarShop.Domain.Entities;
using CarShop.Shared.Data;
using CarShop.Shared.Repositories.Interfaces;

namespace CarShop.Shared.Repositories;

public class CarRepository : Repository<Car>, ICarRepository
{
    public CarRepository(ApplicationDbContext context) : base(context)
    {
    }

}
