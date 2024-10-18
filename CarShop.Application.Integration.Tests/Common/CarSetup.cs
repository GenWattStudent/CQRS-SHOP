using Bogus;
using CarShop.Domain.Entities;

namespace CarShop.Integration.Tests.Common;

public class CarSetup
{
    private readonly Faker<Car> _carFaker;

    public CarSetup()
    {
        _carFaker = new Faker<Car>()
            .RuleFor(c => c.Brand, f => f.Vehicle.Manufacturer())
            .RuleFor(c => c.Model, f => f.Vehicle.Model())
            .RuleFor(c => c.Year, f => f.Date.Past(3).Year)
            .RuleFor(c => c.Color, f => f.Commerce.Color())
            .RuleFor(c => c.Price, f => f.Random.Decimal(10000, 50000))
            .RuleFor(c => c.VIN, f => f.Vehicle.Vin());
    }

    public Car Create()
    {
        return _carFaker.Generate();
    }

    public Car Create(string brand, string model, int year, string color, decimal price, string vin)
    {
        return new Car
        {
            Brand = brand,
            Model = model,
            Year = year,
            Color = color,
            Price = price,
            VIN = vin,
        };
    }

    public Car Update(int id)
    {
        var car = _carFaker.Generate();
        car.Id = id;
        return car;
    }
}
