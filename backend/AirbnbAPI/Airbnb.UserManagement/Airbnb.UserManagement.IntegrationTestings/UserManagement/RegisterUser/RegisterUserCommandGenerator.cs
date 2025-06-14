using Airbnb.IntegrationTesting.Generators;
using Airbnb.IntegrationTesting.Helpers;
using Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Commands.RegisterUserCommand;
using Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Commands.UserCreateCommand;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Aggregates;
using Microsoft.AspNetCore.Http;

namespace Airbnb.UserManagement.IntegrationTestings;

public class RegisterUserCommandGenerator
    : DataGenerator<RegisterUserCommand>
{
    protected override IEnumerable<RegisterUserCommand> GetData()
    {
        var testImage = FileHelper.LoadFormFile("TestData", "ProductAvatar.png");

        yield return new RegisterUserCommand
        {
            FullName    = "Тест Тестов",
            Email       = "test2@example.com",
            DateOfBirth = new DateTime(1980, 1, 1),
            UserPicture = testImage,
            Password = "123456",
            Username = "Test1"
        };

        yield return new RegisterUserCommand
        {
            FullName    = "Сергей Петров",
            Email       = "sergey.petrov@example.com",
            DateOfBirth = new DateTime(1995, 12, 12),
            UserPicture = testImage,
            Password = "123456",
            Username = "Test2"
        };
    }
}