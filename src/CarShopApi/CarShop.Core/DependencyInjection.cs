using CarShop.Core.Presistence;
using CarShop.Shared.Data;
using Microsoft.Extensions.DependencyInjection;

namespace CarShop.Core;

public static class DependencyInjection
{
    public static void AddCore(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<DataSeeder>();
        services.AddDbContext<ApplicationDbContext>(options =>
        {

        });
    }
}
