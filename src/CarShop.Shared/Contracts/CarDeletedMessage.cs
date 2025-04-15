namespace CarShop.Shared.Contracts;

public record CarDeletedMessage(
    Guid Id,
    DateTime DeletedAt
);