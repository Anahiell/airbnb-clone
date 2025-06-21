using Airbnb.Connection.ConnectionService.HttpConnection.Services;
using Airbnb.IntegrationTesting.WebApplicationFactory;
using Airbnb.SharedKernel.ConnectionService.HttpConnection;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Airbnb.ProductManagement.IntegrationTesting.ReviewCreateCommand;

[Collection("04_Product_Review")]
public class ReviewTests : IClassFixture<CustomWebApplicationFactory<Program, ReviewDbContext>>
{
    private readonly IHttpConnectionService _connection;

    public ReviewTests(CustomWebApplicationFactory<Program, ReviewDbContext> factory)
    {
        factory.UseInMemoryDb = true;
        _connection = factory.Services.CreateScope().ServiceProvider.GetRequiredService<IHttpConnectionService>();
    }

    [Theory]
    [ClassData(typeof(ReviewCreateCommandGenerator))]
    public async Task Should_Create_Review_Successfully(Dictionary<string, string> queryParams)
    {
        var response = await _connection.PostAsync<object, int>(
            "/product/api/v1/Review/CreateReviewAsync",
            new HttpConnectionData { ClientName = "ProductService" },
            body: null,
            queryParams: queryParams);

        Assert.True(response > 0);
    }
}