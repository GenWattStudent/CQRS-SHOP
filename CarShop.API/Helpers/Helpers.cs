using CarShop.Shared.Data;

namespace CarShop.Presentation.Helpers;

public static class Helpers
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add services to the container.
        services.AddSingleton<IUnitOfWork, UnitOfWork>();
        services.AddScoped<DataSeeder>();
    }
}
