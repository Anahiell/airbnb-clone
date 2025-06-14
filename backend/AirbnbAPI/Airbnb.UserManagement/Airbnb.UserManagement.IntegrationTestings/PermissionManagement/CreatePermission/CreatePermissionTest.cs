using Airbnb.Connection.ConnectionService.HttpConnection.Services;
using Airbnb.IntegrationTesting.WebApplicationFactory;
using Airbnb.SharedKernel.ConnectionService.HttpConnection;
using Airbnb.UserManagement.Application.BoundedContexts.PermissionManagement.Commands.CreatePermissionCommand;
using Airbnb.UserManagement.Application.BoundedContexts.PermissionManagement.QueryObjects;
using Airbnb.UserManagement.Infrastructure.DataContext;
using Microsoft.Extensions.DependencyInjection;

namespace Airbnb.UserManagement.IntegrationTestings.PermissionManagement.CreatePermission;

[Collection("02_Create_Permissions")]
public class CreatePermissionTest : IClassFixture<CustomWebApplicationFactory<Program, ApplicationDbContext>>
{
    private readonly IHttpConnectionService _connection;

    public CreatePermissionTest(CustomWebApplicationFactory<Program, ApplicationDbContext> factory)
    {
        factory.UseInMemoryDb = true;
        _connection = factory.Services.CreateScope().ServiceProvider.GetRequiredService<IHttpConnectionService>();
    }

    [Theory]
    [ClassData(typeof(CreatePermissionCommandGenerator))]
    public async Task Should_Create_Permission(CreatePermissionCommand cmd)
    {
        var permissionParams = new
        {
            cmd.Name,
        };

        var id = await _connection.PostAsync<CreatePermissionCommand, int>(
            "/api/v1/Permission/CreatePermission",
            new HttpConnectionData { ClientName = "UserService" },
            body: cmd,
            permissionParams);
        
        Assert.True(id > 0, "Expected created language Id to be greater than 0");
    }
}