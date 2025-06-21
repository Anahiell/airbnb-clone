using Airbnb.Connection.ConnectionService.HttpConnection.Services;
using Airbnb.Infrastructure.DataContext;
using Airbnb.IntegrationTesting.WebApplicationFactory;
using Airbnb.SharedKernel.ConnectionService.HttpConnection;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Airbnb.ProductManagement.IntegrationTesting.FeatureCreateCommand;

[Collection("02_Product_Feature")]
public class FeatureTests : IClassFixture<CustomWebApplicationFactory<Program, AirbnbDbContext>>
{
    private readonly IHttpConnectionService _connection;

    public FeatureTests(CustomWebApplicationFactory<Program, AirbnbDbContext> factory)
    {
        factory.UseInMemoryDb = true;
        var scope = factory.Services.CreateScope();
        _connection = scope.ServiceProvider.GetRequiredService<IHttpConnectionService>();
    }

    [Theory]
    [ClassData(typeof(FeatureCreateCommandGenerator))]
    public async Task Should_Create_Feature_Successfully(Dictionary<string, string> queryParams)
    {
        var response = await _connection.PostAsync<object, int>(
            "/product/api/v1/Feature/CreateFeatureAsync",
            new HttpConnectionData { ClientName = "ProductService" },
            body: null,
            query: queryParams);

        Assert.True(response > 0);
    }
}