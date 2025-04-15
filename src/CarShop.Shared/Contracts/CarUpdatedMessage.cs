namespace CarShop.Shared.Contracts;

public record CarUpdatedMessage(
    Guid Id,
    string Brand,
    string Model,
    int Year,
    string Color,
    string Vin,
    decimal Price,
    DateTime UpdatedAt
);