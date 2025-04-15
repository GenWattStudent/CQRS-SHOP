using CarShop.Core.Events;
using CarShop.Core.Presistence;
using CarShop.Domain.Events;
using CarShop.Shared.Data;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace CarShop.Core;

public static class DependencyInjection
{
    public static void AddCore(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<DataSeeder>();
        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
        services.AddScoped<IDomainEventPublisher, MassTransitEventPublisher>();

        // Configure MassTransit with RabbitMQ
        services.AddMassTransit(config =>
        {
            config.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ConfigureEndpoints(context);
            });
        });

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            // Configuration is already in the ApplicationDbContext
        });
    }
}
