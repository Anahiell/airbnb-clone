using Airbnb.IntegrationTesting.Generators;

namespace Airbnb.ProductManagement.IntegrationTesting.TagCreateCommand;

public class ProductTagCommandGenerator : DataGenerator<(int productId, int tagId)>
{
    protected override IEnumerable<(int productId, int tagId)> GetData()
    {
        yield return (1, 1);
    }
}