using CarShop.Application.Behaviours;
using CarShop.Application.Handlers.CarHandlers;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CarShop.Application;

public static class DependancyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(CreateCarCommandHandler).Assembly);
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(typeof(CreateCarCommandHandler).Assembly);
        services.AddAutoMapper(typeof(CreateCarCommandHandler).Assembly);
    }
}
