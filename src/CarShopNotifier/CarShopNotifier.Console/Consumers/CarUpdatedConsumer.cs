using CarShop.Shared.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace CarShopNotifier.Console.Consumers;

public class CarUpdatedConsumer : IConsumer<CarUpdatedMessage>
{
    private readonly ILogger<CarUpdatedConsumer> _logger;

    public CarUpdatedConsumer(ILogger<CarUpdatedConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<CarUpdatedMessage> context)
    {
        var message = context.Message;

        _logger.LogInformation("Car updated: {Brand} {Model} ({Year}) - Color: {Color}, VIN: {VIN}, Price: {Price:C}",
            message.Brand,
            message.Model,
            message.Year,
            message.Color,
            message.Vin,
            message.Price);

        System.Console.ForegroundColor = ConsoleColor.Yellow;
        System.Console.WriteLine("╔═════════════════════════════════════╗");
        System.Console.WriteLine("║           CAR UPDATED               ║");
        System.Console.WriteLine("╚═════════════════════════════════════╝");
        System.Console.WriteLine($"ID: {message.Id}");
        System.Console.WriteLine($"Brand: {message.Brand}");
        System.Console.WriteLine($"Model: {message.Model}");
        System.Console.WriteLine($"Year: {message.Year}");
        System.Console.WriteLine($"Color: {message.Color}");
        System.Console.WriteLine($"VIN: {message.Vin}");
        System.Console.WriteLine($"Price: {message.Price:C}");
        System.Console.WriteLine($"Updated at: {message.UpdatedAt}");
        System.Console.WriteLine("─────────────────────────────────────");
        System.Console.ResetColor();

        return Task.CompletedTask;
    }
}