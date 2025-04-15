using CarShop.Domain.Entities;
using CarShop.Domain.ValueObjects;
using CarShop.Shared.Data.Entities;

namespace CarShop.Shared.Data.Mapping;

public static class EntityMapper
{
    public static CarEntity ToEntity(Car domainCar)
    {
        return new CarEntity
        {
            Id = domainCar.Id,
            Brand = domainCar.Brand,
            Model = domainCar.Model,
            Year = domainCar.Year,
            Color = domainCar.Color,
            VIN = domainCar.VIN.Value,
            Price = domainCar.Price.Amount,
            ImageUrl = domainCar.ImageUrl,
            CreatedAt = domainCar.CreatedAt,
            UpdatedAt = domainCar.UpdatedAt
        };
    }

    public static Car ToDomain(CarEntity entity)
    {
        var car = Activator.CreateInstance(typeof(Car), true) as Car;

        if (car != null)
        {
            typeof(Car).GetProperty(nameof(AggregateRoot.Id))?.SetValue(car, entity.Id);
            typeof(Car).GetProperty(nameof(AggregateRoot.CreatedAt))?.SetValue(car, entity.CreatedAt);
            typeof(Car).GetProperty(nameof(AggregateRoot.UpdatedAt))?.SetValue(car, entity.UpdatedAt);

            typeof(Car).GetProperty(nameof(Car.Brand))?.SetValue(car, entity.Brand);
            typeof(Car).GetProperty(nameof(Car.Model))?.SetValue(car, entity.Model);
            typeof(Car).GetProperty(nameof(Car.Year))?.SetValue(car, entity.Year);
            typeof(Car).GetProperty(nameof(Car.Color))?.SetValue(car, entity.Color);
            typeof(Car).GetProperty(nameof(Car.VIN))?.SetValue(car, VIN.Create(entity.VIN));
            typeof(Car).GetProperty(nameof(Car.Price))?.SetValue(car, Money.From(entity.Price));
            typeof(Car).GetProperty(nameof(Car.ImageUrl))?.SetValue(car, entity.ImageUrl);
        }

        return car;
    }
}