using Airbnb.IntegrationTesting.Generators;

namespace Airbnb.ProductManagement.IntegrationTesting.FacilityCreateCommand;

public class FacilityCreateCommandGenerator : DataGenerator<Dictionary<string, string>>
{
    protected override IEnumerable<Dictionary<string, string>> GetData()
    {
        yield return new()
        {
            ["Name"] = "Бассейн",
            ["IconName"] = "pool"
        };
        yield return new()
        {
            ["Name"] = "Фитнес-зал",
            ["IconName"] = "fitness"
        };
    }
}