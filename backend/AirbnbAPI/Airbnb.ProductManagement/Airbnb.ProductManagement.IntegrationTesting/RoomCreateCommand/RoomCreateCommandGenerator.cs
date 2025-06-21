using Airbnb.IntegrationTesting.Generators;

namespace Airbnb.ProductManagement.IntegrationTesting.RoomCreateCommand;

public class RoomCreateCommandGenerator : DataGenerator<Dictionary<string, string>>
{
    protected override IEnumerable<Dictionary<string, string>> GetData()
    {
        yield return new()
        {
            ["ProductId"] = "1",
            ["Name"] = "Спальня",
            ["Photo"] = "https://img.example.com/bedroom.jpg",
            ["Capacity"] = "2"
        };
        yield return new()
        {
            ["ProductId"] = "1",
            ["Name"] = "Гостиная",
            ["Photo"] = "https://img.example.com/living.jpg",
            ["Capacity"] = "4"
        };
    }
}