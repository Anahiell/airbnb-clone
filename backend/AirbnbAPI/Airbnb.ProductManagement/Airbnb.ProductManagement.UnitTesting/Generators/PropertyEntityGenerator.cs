using Airbnb.Domain;
using Airbnb.Infrastructure.Entities;

namespace Airbnb.UnitTesting.Generators;

public class PropertyEntityGenerator : DataGenerator<DomainProduct>
{
    public List<DomainProduct> GetGeneratedData()
    {
        return GetData().ToList();
    }

    protected override IEnumerable<DomainProduct> GetData()
    {
        yield return new DomainProduct()
        {
            // Id = 1,
        };
        yield return new DomainProduct()
        {
            // Id = 2,
        };
        yield return new DomainProduct()
        {
            // Id = 3,
        };
    }
}