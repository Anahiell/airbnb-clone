using Airbnb.Connection.ConnectionService.HttpConnection.Services;
using Airbnb.IntegrationTesting.WebApplicationFactory;
using Airbnb.SharedKernel.ConnectionService.HttpConnection;
using Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Commands.UpdateUserCommand;
using Airbnb.UserManagement.Infrastructure.DataContext;
using Microsoft.Extensions.DependencyInjection;

namespace Airbnb.UserManagement.IntegrationTestings.UserManagement.UpdateUser;

[Collection("05_Update_User")]
public class UpdateUserTest : IClassFixture<CustomWebApplicationFactory<Program, ApplicationDbContext>>
{
    private readonly CustomWebApplicationFactory<Program, ApplicationDbContext> _factory;
    private readonly IHttpConnectionService _connection;

    public UpdateUserTest(CustomWebApplicationFactory<Program, ApplicationDbContext> factory)
    {
        _factory = factory;
        _factory.UseInMemoryDb = true;

        var scope = _factory.Services.CreateScope();
        _connection = scope.ServiceProvider.GetRequiredService<IHttpConnectionService>();
    }
    

    [Theory]
    [ClassData(typeof(UpdateUserCommandGenerator))]
    public async Task Should_Update_User_Successfully(UpdateUserCommand cmd)
    {
        var response = await _connection.PutAsync<UpdateUserCommand, string>(
            "/api/v1/User/UpdateUser", new HttpConnectionData { ClientName = "UserService" }, body: cmd, serializeEnumsAsStrings: true);

        Assert.Equal("Пользователь успешно обновлен", response);
    }
}