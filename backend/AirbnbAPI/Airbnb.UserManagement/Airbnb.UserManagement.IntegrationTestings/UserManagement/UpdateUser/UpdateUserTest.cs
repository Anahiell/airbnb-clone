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
        var queryParams = new
        {
            cmd.Id,
            cmd.FullName,
            cmd.Email,
            Roles = cmd.Roles?.Select(x => x.ToString()).ToArray(),
            Permissions = cmd.Permissions?.Select(x => x.ToString()).ToArray(),
            Languages   = cmd.Languages?.Select(x => x.ToString()).ToArray(),
            cmd.DateOfBirth,
            Profile = new
            {
                cmd.Profile?.School,
                cmd.Profile?.Location,
                cmd.Profile?.Hobbies,
                cmd.Profile?.LifeGoals,
                cmd.Profile?.TimeSpentOn,
                cmd.Profile?.Profession,
                cmd.Profile?.FavSong,
                cmd.Profile?.FunFact,
                cmd.Profile?.BioTitle,
                cmd.Profile?.Pets,
                cmd.Profile?.About
            }
        };
        
        if (cmd.Roles != null)
            foreach (var (role, i) in cmd.Roles.Select((v, i) => (v, i)))
                queryParams[$"Roles[{i}]"] = role;

        if (cmd.Permissions != null)
            foreach (var (perm, i) in cmd.Permissions.Select((v, i) => (v, i)))
                query[$"Permissions[{i}]"] = perm;

        if (cmd.Languages != null)
            foreach (var (lang, i) in cmd.Languages.Select((v, i) => (v, i)))
                query[$"Languages[{i}]"] = lang;
        
        var response = await _connection.PutAsync<UpdateUserCommand, string>(
            "/api/v1/User/UpdateUser", new HttpConnectionData { ClientName = "UserService" }, query: queryParams, serializeEnumsAsStrings: true);

        Assert.Equal("Пользователь успешно обновлен", response);
    }
}