using Airbnb.IntegrationTesting.Generators;
using Airbnb.UserManagement.Application.BoundedContexts.LanguageManagement.Commands.CreateLanguageCommand;

namespace Airbnb.UserManagement.IntegrationTestings.LanguageManagement.CreateLanguage;

public class CreateLanguageCommandGenerator : DataGenerator<CreateLanguageCommand>
{
    protected override IEnumerable<CreateLanguageCommand> GetData()
    {
        yield return new CreateLanguageCommand { Name = "English" };
        yield return new CreateLanguageCommand { Name = "Ukraine" };
    }
}