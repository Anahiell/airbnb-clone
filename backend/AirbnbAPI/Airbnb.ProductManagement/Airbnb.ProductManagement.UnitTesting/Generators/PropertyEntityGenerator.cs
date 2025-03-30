using Airbnb.Infrastructure.Entities;

namespace Airbnb.UnitTesting.Generators;

public class PropertyEntityGenerator : DataGenerator<PropertyEntity>
{
    public List<PropertyEntity> GetGeneratedData()
    {
        return GetData().ToList();
    }

    protected override IEnumerable<PropertyEntity> GetData()
    {
        yield return new PropertyEntity()
        {
            Id = 1,
        };
        yield return new PropertyEntity()
        {
            Id = 2,
        };
        yield return new PropertyEntity()
        {
            Id = 3,
        };
    }
}