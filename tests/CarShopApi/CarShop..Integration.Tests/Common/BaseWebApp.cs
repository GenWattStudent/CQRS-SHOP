using Microsoft.AspNetCore.Mvc.Testing;

namespace CarShop.Integration.Tests.Common;

public class BaseWebApp : IClassFixture<WebApplicationFactory<Program>>
{
    protected readonly WebApplicationFactory<Program> _factory;
    protected readonly HttpClient _client;

    public BaseWebApp()
    {
        _factory = new WebApplicationFactory<Program>();
        _client = _factory.CreateClient();
    }
}
