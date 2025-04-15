using CarShop.Shared.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace CarShopNotifier.Console.Consumers;

public class CarDeletedConsumer : IConsumer<CarDeletedMessage>
{
    private readonly ILogger<CarDeletedConsumer> _logger;

    public CarDeletedConsumer(ILogger<CarDeletedConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<CarDeletedMessage> context)
    {
        var message = context.Message;

        _logger.LogInformation("Car deleted: ID {Id}", message.Id);

        System.Console.ForegroundColor = ConsoleColor.Red;
        System.Console.WriteLine("╔═════════════════════════════════════╗");
        System.Console.WriteLine("║           CAR DELETED               ║");
        System.Console.WriteLine("╚═════════════════════════════════════╝");
        System.Console.WriteLine($"ID: {message.Id}");
        System.Console.WriteLine($"Deleted at: {message.DeletedAt}");
        System.Console.WriteLine("─────────────────────────────────────");
        System.Console.ResetColor();

        return Task.CompletedTask;
    }
}