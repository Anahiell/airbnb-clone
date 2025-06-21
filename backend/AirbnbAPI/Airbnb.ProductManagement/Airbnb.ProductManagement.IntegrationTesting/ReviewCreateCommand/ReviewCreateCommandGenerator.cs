using Airbnb.IntegrationTesting.Generators;

namespace Airbnb.ProductManagement.IntegrationTesting.ReviewCreateCommand;

public class ReviewCreateCommandGenerator : DataGenerator<Dictionary<string, string>>
{
    protected override IEnumerable<Dictionary<string, string>> GetData()
    {
        yield return new()
        {
            ["ProductId"] = "1",
            ["UserId"] = "1",
            ["Comment"] = "Очень чисто и уютно!",
            ["Rating"] = "5"
        };
    }
}