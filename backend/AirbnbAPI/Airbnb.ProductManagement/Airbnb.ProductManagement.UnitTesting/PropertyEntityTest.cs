using Airbnb.Domain;
using Airbnb.Infrastructure.Entities;
using Airbnb.UnitTesting.Generators;

namespace Airbnb.UnitTesting;

public class PropertyEntityTest
{
    [Theory]
    [ClassData(typeof(PropertyEntityGenerator))]
    public void Test1(DomainProduct property)
    {
        Assert.NotNull(property);
        Assert.True(property.Id > 0);
    }
}