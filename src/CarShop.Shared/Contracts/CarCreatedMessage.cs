namespace CarShop.Shared.Contracts;

public record CarCreatedMessage(
    Guid Id,
    string Brand,
    string Model,
    int Year,
    string Color,
    string Vin,
    decimal Price,
    DateTime CreatedAt
);
