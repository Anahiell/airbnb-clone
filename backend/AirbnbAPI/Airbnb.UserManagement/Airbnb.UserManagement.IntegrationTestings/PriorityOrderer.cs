using Xunit.Abstractions;

namespace Airbnb.UserManagement.IntegrationTestings;

public class PriorityOrderer : ITestCollectionOrderer
{
    public IEnumerable<ITestCollection> OrderTestCollections(IEnumerable<ITestCollection> testCollections)
    {
        return testCollections.OrderBy(c => c.DisplayName);
    }
}