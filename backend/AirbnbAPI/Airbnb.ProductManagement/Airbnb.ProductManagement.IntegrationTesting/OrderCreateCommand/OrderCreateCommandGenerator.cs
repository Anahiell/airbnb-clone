using Airbnb.IntegrationTesting.Generators;

namespace Airbnb.ProductManagement.IntegrationTesting.OrderCreateCommand;

public class OrderCreateCommandGenerator : DataGenerator<Dictionary<string, string>>
{
    protected override IEnumerable<Dictionary<string, string>> GetData()
    {
        yield return new()
        {
            ["ProductId"] = "1",
            ["UserId"] = "2",
            ["DateStart"] = DateTime.UtcNow.AddDays(5).ToString("O"),
            ["DateEnd"] = DateTime.UtcNow.AddDays(10).ToString("O"),
            ["GuestsCount"] = "3"
        };
    }
}