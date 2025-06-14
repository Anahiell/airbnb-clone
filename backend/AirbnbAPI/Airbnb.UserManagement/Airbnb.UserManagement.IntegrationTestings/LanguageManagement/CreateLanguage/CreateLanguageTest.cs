using Airbnb.Connection.ConnectionService.HttpConnection.Services;
using Airbnb.IntegrationTesting.WebApplicationFactory;
using Airbnb.SharedKernel.ConnectionService.HttpConnection;
using Airbnb.UserManagement.Application.BoundedContexts.LanguageManagement.Commands.CreateLanguageCommand;
using Airbnb.UserManagement.Application.BoundedContexts.UserLangauageManagement.QueryObjects;
using Airbnb.UserManagement.Infrastructure.DataContext;
using Microsoft.Extensions.DependencyInjection;

namespace Airbnb.UserManagement.IntegrationTestings.LanguageManagement.CreateLanguage;

[Collection("01_Create_Language")]
public class CreateLanguageTest : IClassFixture<CustomWebApplicationFactory<Program, ApplicationDbContext>>
{
    private readonly IHttpConnectionService _connection;

    public CreateLanguageTest(CustomWebApplicationFactory<Program, ApplicationDbContext> factory)
    {
        factory.UseInMemoryDb = true;
        _connection = factory.Services.CreateScope().ServiceProvider.GetRequiredService<IHttpConnectionService>();
    }

    [Theory]
    [ClassData(typeof(CreateLanguageCommandGenerator))]
    public async Task Should_Create_Language(CreateLanguageCommand cmd)
    {
        var languageParams = new
        {
            cmd.Name,
        };

        var id = await _connection.PostAsync<CreateLanguageCommand, int>(
            "/api/v1/Language/CreateLanguage",
            new HttpConnectionData { ClientName = "UserService" },
            body: cmd,
            languageParams);
        
        Assert.True(id > 0, "Expected created language Id to be greater than 0");
    }
}