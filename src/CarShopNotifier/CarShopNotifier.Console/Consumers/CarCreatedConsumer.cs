using CarShop.Shared.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace CarShopNotifier.Console.Consumers;

public class CarCreatedConsumer : IConsumer<CarCreatedMessage>
{
    private readonly ILogger<CarCreatedConsumer> _logger;
    private static readonly SemaphoreSlim _consoleLock = new SemaphoreSlim(1, 1);

    public CarCreatedConsumer(ILogger<CarCreatedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<CarCreatedMessage> context)
    {
        var message = context.Message;

        _logger.LogInformation("Car created: {Brand} {Model} ({Year}) - Color: {Color}, VIN: {VIN}, Price: {Price:C}",
            message.Brand,
            message.Model,
            message.Year,
            message.Color,
            message.Vin,
            message.Price);

        // Use a lock to ensure complete output without interleaving
        await _consoleLock.WaitAsync();
        try
        {
            // Store current console color to restore later
            var originalColor = System.Console.ForegroundColor;

            System.Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine("\n╔═════════════════════════════════════╗");
            System.Console.WriteLine("║           NEW CAR CREATED           ║");
            System.Console.WriteLine("╚═════════════════════════════════════╝");
            System.Console.WriteLine($"ID: {message.Id}");
            System.Console.WriteLine($"Brand: {message.Brand}");
            System.Console.WriteLine($"Model: {message.Model}");
            System.Console.WriteLine($"Year: {message.Year}");
            System.Console.WriteLine($"Color: {message.Color}");
            System.Console.WriteLine($"VIN: {message.Vin}");
            System.Console.WriteLine($"Price: {message.Price:C}");
            System.Console.WriteLine($"Created at: {message.CreatedAt}");
            System.Console.WriteLine("─────────────────────────────────────\n");

            // Restore original console color
            System.Console.ForegroundColor = originalColor;
        }
        finally
        {
            // Always release the lock
            _consoleLock.Release();
        }
    }
}