using CarShop.Application.Behaviours;
using CarShop.Application.Handlers.CarHandlers;
using CarShop.Shared.Data;
using FluentValidation;

namespace CarShop.Presentation.Helpers;

public static class Helpers
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add services to the container.
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<DataSeeder>();

        services.AddValidatorsFromAssembly(typeof(CreateCarCommandHandler).Assembly);
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(CreateCarCommandHandler).Assembly);
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        services.AddAutoMapper(typeof(CreateCarCommandHandler).Assembly);

        services.AddDbContext<ApplicationDbContext>(options =>
        {

        });
    }
}
