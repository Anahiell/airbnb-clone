using Airbnb.IntegrationTesting.Generators;

namespace Airbnb.ProductManagement.IntegrationTesting.ProductCreateCommand;

public class ProductCreateCommandGenerator : DataGenerator<object>
{
    protected override IEnumerable<object> GetData()
    {
        yield return new
        {
            Title = "Апартаменты с видом на море",
            Description = "Просторные и светлые апартаменты рядом с пляжем",
            Address = "Одесса, Французский бульвар, 1",
            PricePerNight = 2000,
            OwnerId = 1,
            Coordinates = new { Latitude = 46.4775, Longitude = 30.7326 },
            Rules = new { NoSmoking = true, NoPets = false, QuietHours = "22:00–07:00" },
            ImportantInfo = new { CheckIn = "14:00", CheckOut = "11:00", Deposit = 1000 }
        };
    }
}