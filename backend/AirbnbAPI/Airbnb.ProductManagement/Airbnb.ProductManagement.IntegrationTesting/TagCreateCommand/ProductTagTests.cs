using Airbnb.Connection.ConnectionService.HttpConnection.Services;
using Airbnb.IntegrationTesting.WebApplicationFactory;
using Airbnb.SharedKernel.ConnectionService.HttpConnection;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Airbnb.ProductManagement.IntegrationTesting.TagCreateCommand;

[Collection("03_ProductTag")]
public class ProductTagTests : IClassFixture<CustomWebApplicationFactory<Program, TagDbContext>>
{
    private readonly IHttpConnectionService _connection;

    public ProductTagTests(CustomWebApplicationFactory<Program, TagDbContext> factory)
    {
        factory.UseInMemoryDb = true;
        _connection = factory.Services.CreateScope().ServiceProvider.GetRequiredService<IHttpConnectionService>();
    }

    [Theory]
    [ClassData(typeof(ProductTagCommandGenerator))]
    public async Task Should_Create_ProductTag_Successfully((int productId, int tagId) ids)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["ProductId"] = ids.productId.ToString(),
            ["TagId"] = ids.tagId.ToString()
        };

        var response = await _connection.PostAsync<object, int>(
            "/tag/api/v1/ProductTag/CreateProductTag",
            new HttpConnectionData { ClientName = "TagService" },
            body: null,
            queryParams: queryParams);

        Assert.True(response > 0);
    }
}