using Airbnb.Connection.ConnectionService.HttpConnection.Services;
using Airbnb.IntegrationTesting.WebApplicationFactory;
using Airbnb.SharedKernel.ConnectionService.HttpConnection;
using Airbnb.UserManagement.Application.BoundedContexts.RoleManagement.Commands.CreateRoleCommand;
using Airbnb.UserManagement.Application.BoundedContexts.RoleManagement.QueryObjects;
using Airbnb.UserManagement.Infrastructure.DataContext;
using Airbnb.UserManagement.IntegrationTestings.Roles.CreateRole;
using Microsoft.Extensions.DependencyInjection;

namespace Airbnb.UserManagement.IntegrationTestings.LanguageManagement.CreateLanguage;

[Collection("03_CreateRoles")]
public class CreateRoleTest : IClassFixture<CustomWebApplicationFactory<Program, ApplicationDbContext>>
{
    private readonly IHttpConnectionService _connection;

    public CreateRoleTest(CustomWebApplicationFactory<Program, ApplicationDbContext> factory)
    {
        factory.UseInMemoryDb = true;
        _connection = factory.Services.CreateScope().ServiceProvider.GetRequiredService<IHttpConnectionService>();
    }

    [Theory]
    [ClassData(typeof(CreateRoleCommandGenerator))]
    public async Task Should_Create_Role(CreateRoleCommand cmd)
    {
        var roleParams = new
        {
            cmd.Name,
        };

        var id = await _connection.PostAsync<CreateRoleCommand, int>(
            "/api/v1/Role/CreateRole",
            new HttpConnectionData { ClientName = "UserService" },
            body: cmd,
            roleParams);
        
        Assert.True(id > 0, "Expected created language Id to be greater than 0");
    }
}