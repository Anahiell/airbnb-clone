using Airbnb.IntegrationTesting.Generators;
using Airbnb.UserManagement.Application.BoundedContexts.PermissionManagement.Commands.CreatePermissionCommand;

namespace Airbnb.UserManagement.IntegrationTestings.PermissionManagement.CreatePermission;

public class CreatePermissionCommandGenerator : DataGenerator<CreatePermissionCommand>
{
    protected override IEnumerable<CreatePermissionCommand> GetData()
    {
        yield return new CreatePermissionCommand { Name = "CanUseAdminController" };
        yield return new CreatePermissionCommand { Name = "CanAnswerSupportRequest" };
    }
}