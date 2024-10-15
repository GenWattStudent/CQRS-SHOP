using CarShop.Domain.Entities;

namespace CarShop.Shared.Data;

public class DataSeeder
{
    private readonly IUnitOfWork _unitOfWork;

    public DataSeeder(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task SeedData()
    {
        _unitOfWork.Cars.AddRange(new List<Car>
        {
            new Car
            {
                Brand = "Toyota",
                Model = "Corolla",
                Year = 2020,
                Price = 20000,
                VIN = "5YFBURHE0LP100000",
                Color = "Silver"
            },
            new Car
            {
                Brand = "Honda",
                Model = "Civic",
                Year = 2019,
                Price = 18000,
                VIN = "19XFC2F50KE200000",
                Color = "Blue"
            },
            new Car
            {
                Brand = "Ford",
                Model = "Fusion",
                Year = 2018,
                Price = 22000,
                VIN = "3FA6P0HD9JR100001",
                Color = "Red"
            },
            new Car
            {
                Brand = "Chevrolet",
                Model = "Malibu",
                Year = 2017,
                Price = 19000,
                VIN = "1G1ZD5STXHF140206",
                Color = "White"
            },
            new Car
            {
                Brand = "Nissan",
                Model = "Altima",
                Year = 2016,
                Price = 21000,
                VIN = "1N4AL30T5GC110054",
                Color = "Black"
            }
        });

        await _unitOfWork.SaveChangesAsync();
    }
}
