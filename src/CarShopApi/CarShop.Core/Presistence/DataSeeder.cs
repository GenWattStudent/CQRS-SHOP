using CarShop.Domain.Entities;

namespace CarShop.Core.Presistence;

public class DataSeeder
{
    private readonly IUnitOfWork _unitOfWork;

    public DataSeeder(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task SeedData()
    {
        var carsToSeed = new List<Car>
        {
            new Car(
                brand: "Toyota",
                model: "Corolla",
                year: 2020,
                color: "Silver",
                vin: "5YFBURHE0LP100000",
                price: 20000
            ),

            new Car(
                brand: "Honda",
                model: "Civic",
                year: 2019,
                color: "Blue",
                vin: "19XFC2F50KE200000",
                price: 18000
            ),

            new Car(
                brand: "Ford",
                model: "Fusion",
                year: 2018,
                color: "Red",
                vin: "3FA6P0HD9JR100001",
                price: 22000
            ),

            new Car(
                brand: "Chevrolet",
                model: "Malibu",
                year: 2017,
                color: "White",
                vin: "1G1ZD5STXHF140206",
                price: 19000
            ),

            new Car(
                brand: "Nissan",
                model: "Altima",
                year: 2016,
                color: "Black",
                vin: "1N4AL30T5GC110054",
                price: 21000
            )
        };

        foreach (var car in carsToSeed)
        {
            _unitOfWork.Cars.Add(car);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}