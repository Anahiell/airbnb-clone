using System.Text.Json;
using Airbnb.Connection.ConnectionService.HttpConnection.Services;
using Airbnb.IntegrationTesting.WebApplicationFactory;
using Airbnb.SharedKernel.ConnectionService.HttpConnection;
using Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Commands.RegisterUserCommand;
using Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Commands.UserCreateCommand;
using Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.QueryObjects;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Aggregates;
using Airbnb.UserManagement.Infrastructure.DataContext;
using Microsoft.Extensions.DependencyInjection;

namespace Airbnb.UserManagement.IntegrationTestings;

[Collection("04_Register_User")]
public class UserTests : IClassFixture<CustomWebApplicationFactory<Program, ApplicationDbContext>>
{
    private readonly CustomWebApplicationFactory<Program, ApplicationDbContext> _factory;
    private readonly IHttpConnectionService  _connection;
    
    public UserTests(CustomWebApplicationFactory<Program, ApplicationDbContext> factory)
    {
        _factory = factory;
        _factory.UseInMemoryDb = true;
        var scope = _factory.Services.CreateScope();
        _connection = scope.ServiceProvider.GetRequiredService<IHttpConnectionService>();
    }

    [Theory]
    [ClassData(typeof(RegisterUserCommandGenerator))]
    public async Task Should_Create_User_Successfully(RegisterUserCommand cmd)
    {
        // var route = "/api/v1/Auth/Register?Roles=Customer&Roles=Admin&FullName=Сергей Петров&Email=sergey.petrov@example.com&Password=123456&DateOfBirth=12.12.1995";

        var queryParams = new
        {
            cmd.FullName,
            cmd.Email,
            cmd.Password,
            cmd.DateOfBirth,
            cmd.Username,
            cmd.UserPicture
        };

        var response = await _connection.PostAsync<RegisterUserCommand, int>(
            $"/api/v1/Auth/Register", new HttpConnectionData { ClientName = "UserService" }, body: cmd, queryParams);

        Assert.True(response > 0, "Expected created language Id to be greater than 0");
    }
}