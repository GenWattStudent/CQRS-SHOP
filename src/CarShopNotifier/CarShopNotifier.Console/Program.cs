using CarShopNotifier.Console.Consumers;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

// Create and configure the host
var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        // Configure MassTransit with RabbitMQ
        services.AddMassTransit(config =>
        {
            // Register consumers
            config.AddConsumer<CarCreatedConsumer>();
            config.AddConsumer<CarUpdatedConsumer>();
            config.AddConsumer<CarDeletedConsumer>();

            // Configure RabbitMQ transport
            config.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                // Configure consumer endpoints
                cfg.ConfigureEndpoints(context);
            });
        });
    })
    .ConfigureLogging((hostContext, logging) =>
    {
        logging.ClearProviders();
        logging.AddConsole();
    })
    .Build();

Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine("╔═════════════════════════════════════════════════╗");
Console.WriteLine("║             CAR SHOP NOTIFIER                   ║");
Console.WriteLine("╚═════════════════════════════════════════════════╝");
Console.WriteLine("Listening for car shop events...");
Console.WriteLine("Press Ctrl+C to exit");
Console.WriteLine("─────────────────────────────────────────────────");
Console.ResetColor();

// Run the host
await host.RunAsync();
