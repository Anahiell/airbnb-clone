using Airbnb.IntegrationTesting.Generators;
using Airbnb.UserManagement.Application.BoundedContexts.RoleManagement.Commands.CreateRoleCommand;

namespace Airbnb.UserManagement.IntegrationTestings.Roles.CreateRole;

public class CreateRoleCommandGenerator : DataGenerator<CreateRoleCommand>
{
    protected override IEnumerable<CreateRoleCommand> GetData()
    {
        yield return new CreateRoleCommand { Name = "Admin" };
        yield return new CreateRoleCommand { Name = "Guest" };
    }
}