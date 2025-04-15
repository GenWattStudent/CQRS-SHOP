using CarShop.Domain.Events;
using CarShop.Domain.ValueObjects;

namespace CarShop.Domain.Entities;

public class Car : AggregateRoot
{
    public string Brand { get; private set; } = string.Empty;
    public string Model { get; private set; } = string.Empty;
    public int Year { get; private set; }
    public string Color { get; private set; } = string.Empty;
    public VIN VIN { get; private set; } = null!;
    public Money Price { get; private set; } = null!;
    public string? ImageUrl { get; private set; }

    protected Car() { }

    public Car(string brand, string model, int year, string color, string vin, decimal price, string? imageUrl = null)
    {
        if (string.IsNullOrWhiteSpace(brand))
            throw new ArgumentException("Brand cannot be empty", nameof(brand));

        if (string.IsNullOrWhiteSpace(model))
            throw new ArgumentException("Model cannot be empty", nameof(model));

        if (year <= 0)
            throw new ArgumentException("Year must be a positive integer", nameof(year));

        if (string.IsNullOrWhiteSpace(color))
            throw new ArgumentException("Color cannot be empty", nameof(color));

        if (price <= 0)
            throw new ArgumentException("Price must be greater than zero", nameof(price));

        Id = Guid.NewGuid();
        Brand = brand;
        Model = model;
        Year = year;
        Color = color;
        VIN = VIN.Create(vin);
        Price = Money.From(price);
        ImageUrl = imageUrl;

        AddDomainEvent(new CarCreatedEvent(this));
    }

    public void UpdateDetails(string brand, string model, int year, string color, decimal price, string? imageUrl = null)
    {
        if (string.IsNullOrWhiteSpace(brand))
            throw new ArgumentException("Brand cannot be empty", nameof(brand));

        if (string.IsNullOrWhiteSpace(model))
            throw new ArgumentException("Model cannot be empty", nameof(model));

        if (year <= 0)
            throw new ArgumentException("Year must be a positive integer", nameof(year));

        if (string.IsNullOrWhiteSpace(color))
            throw new ArgumentException("Color cannot be empty", nameof(color));

        if (price <= 0)
            throw new ArgumentException("Price must be greater than zero", nameof(price));

        Brand = brand;
        Model = model;
        Year = year;
        Color = color;
        Price = Money.From(price);
        ImageUrl = imageUrl;
        UpdatedAt = DateTime.Now;

        AddDomainEvent(new CarUpdatedEvent(this));
    }

    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice <= 0)
        {
            throw new ArgumentException("Price must be greater than zero", nameof(newPrice));
        }

        var oldPrice = Price;
        Price = Money.From(newPrice);
        UpdatedAt = DateTime.Now;

        AddDomainEvent(new CarPriceChangedEvent(this, oldPrice.Amount, newPrice));
    }

    public void MarkAsDeleted()
    {
        AddDomainEvent(new CarDeletedEvent(Id));
    }
}
