using CarShop.Application.Abstractions;
using CarShop.Domain.Entities;

namespace CarShop.Application.Commands.CarCommands;

public record UpdateCarCommand(
    int Id, 
    string Brand, 
    string Model, 
    int Year, 
    string Color,
    string VIN, 
    decimal Price, 
    string? ImageUrl) : ICommand<Car>
{
}
