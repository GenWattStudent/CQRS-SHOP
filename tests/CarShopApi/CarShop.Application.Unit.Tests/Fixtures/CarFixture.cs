using CarShop.Application.Unit.Tests.Setups;
using Microsoft.Extensions.DependencyInjection;

namespace CarShop.Application.Unit.Tests.Fixtures;

public class CarFixture : IDisposable
{
    public ServiceProvider ServiceProvider { get; private set; }

    public CarFixture()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddScoped<CarSetup>();

        ServiceProvider = serviceCollection.BuildServiceProvider();
    }

    public void Dispose()
    {
        ServiceProvider.Dispose();
    }
}
