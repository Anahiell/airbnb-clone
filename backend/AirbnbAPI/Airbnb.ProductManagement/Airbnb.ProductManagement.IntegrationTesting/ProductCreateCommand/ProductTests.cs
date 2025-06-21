using Airbnb.Connection.ConnectionService.HttpConnection.Services;
using Airbnb.IntegrationTesting.WebApplicationFactory;
using Airbnb.SharedKernel.ConnectionService.HttpConnection;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Airbnb.ProductManagement.IntegrationTesting.ProductCreateCommand;

[Collection("07_Product_Create")]
public class ProductTests : IClassFixture<CustomWebApplicationFactory<Program, ProductDbContext>>
{
    private readonly IHttpConnectionService _connection;

    public ProductTests(CustomWebApplicationFactory<Program, ProductDbContext> factory)
    {
        factory.UseInMemoryDb = true;
        _connection = factory.Services.CreateScope().ServiceProvider.GetRequiredService<IHttpConnectionService>();
    }

    [Theory]
    [ClassData(typeof(ProductCreateCommandGenerator))]
    public async Task Should_Create_Product_Successfully(object body)
    {
        var response = await _connection.PostAsync<object, int>(
            "/product/api/v1/Product/CreateProductAsync",
            new HttpConnectionData { ClientName = "ProductService" },
            body: body);

        Assert.True(response > 0);
    }
}