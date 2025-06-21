using Airbnb.Connection.ConnectionService.HttpConnection.Services;
using Airbnb.IntegrationTesting.WebApplicationFactory;
using Airbnb.SharedKernel.ConnectionService.HttpConnection;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Airbnb.ProductManagement.IntegrationTesting.RoomCreateCommand;

[Collection("06_Product_Room")]
public class RoomTests : IClassFixture<CustomWebApplicationFactory<Program, ProductDbContext>>
{
    private readonly IHttpConnectionService _connection;

    public RoomTests(CustomWebApplicationFactory<Program, ProductDbContext> factory)
    {
        factory.UseInMemoryDb = true;
        _connection = factory.Services.CreateScope().ServiceProvider.GetRequiredService<IHttpConnectionService>();
    }

    [Theory]
    [ClassData(typeof(RoomCreateCommandGenerator))]
    public async Task Should_Create_Room_Successfully(Dictionary<string, string> queryParams)
    {
        var response = await _connection.PostAsync<object, int>(
            "/product/api/v1/Room/CreateRoomAsync",
            new HttpConnectionData { ClientName = "ProductService" },
            body: null,
            queryParams: queryParams);

        Assert.True(response > 0);
    }
}