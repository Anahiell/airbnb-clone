using Airbnb.Connection.ConnectionService.HttpConnection.Services;
using Airbnb.Infrastructure.DataContext;
using Airbnb.IntegrationTesting.WebApplicationFactory;
using Airbnb.SharedKernel.ConnectionService.HttpConnection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;

namespace Airbnb.ProductManagement.IntegrationTesting.FacilityCreateCommand;

[Collection("01_Product_Facility")]
public class FacilityTests : IClassFixture<CustomWebApplicationFactory<Program, AirbnbDbContext>>
{
    private readonly IHttpConnectionService _connection;

    public FacilityTests(CustomWebApplicationFactory<Program, AirbnbDbContext> factory)
    {
        factory.UseInMemoryDb = true;
        var scope = factory.Services.CreateScope();
        _connection = scope.ServiceProvider.GetRequiredService<IHttpConnectionService>();
    }

    [Theory]
    [ClassData(typeof(FacilityCreateCommandGenerator))]
    public async Task Should_Create_Facility_Successfully(Dictionary<string, string> queryParams)
    {
        var response = await _connection.PostAsync<object, int>(
            "/product/api/v1/Facility/CreateFacilityAsync",
            new HttpConnectionData { ClientName = "ProductService" },
            body: null,
            query: queryParams);

        Assert.True(response > 0, "Facility Id должен быть больше 0");
    }
}