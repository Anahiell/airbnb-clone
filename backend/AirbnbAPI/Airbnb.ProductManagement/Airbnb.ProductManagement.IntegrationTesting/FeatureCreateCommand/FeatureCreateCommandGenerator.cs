using Airbnb.IntegrationTesting.Generators;

namespace Airbnb.ProductManagement.IntegrationTesting.FeatureCreateCommand;

public class FeatureCreateCommandGenerator : DataGenerator<Dictionary<string, string>>
{
    protected override IEnumerable<Dictionary<string, string>> GetData()
    {
        yield return new()
        {
            ["Name"] = "Кондиционер",
            ["Price"] = "50",
            ["Forcibly"] = "true"
        };
    }
}