using Airbnb.Connection.ConnectionService.HttpConnection.Services;
using Airbnb.IntegrationTesting.WebApplicationFactory;
using Airbnb.SharedKernel.ConnectionService.HttpConnection;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Airbnb.ProductManagement.IntegrationTesting.OrderCreateCommand;

[Collection("05_Product_Order")]
public class OrderTests : IClassFixture<CustomWebApplicationFactory<Program, OrderDbContext>>
{
    private readonly IHttpConnectionService _connection;

    public OrderTests(CustomWebApplicationFactory<Program, OrderDbContext> factory)
    {
        factory.UseInMemoryDb = true;
        _connection = factory.Services.CreateScope().ServiceProvider.GetRequiredService<IHttpConnectionService>();
    }

    [Theory]
    [ClassData(typeof(OrderCreateCommandGenerator))]
    public async Task Should_Create_Order_Successfully(Dictionary<string, string> queryParams)
    {
        var response = await _connection.PostAsync<object, int>(
            "/product/api/v1/Order/CreateOrderAsync",
            new HttpConnectionData { ClientName = "ProductService" },
            body: null,
            queryParams: queryParams);

        Assert.True(response > 0);
    }
}