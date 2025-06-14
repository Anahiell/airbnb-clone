using System.Collections;

namespace Airbnb.IntegrationTesting.Generators;

public abstract class DataGenerator<T>
    : IEnumerable<object[]>
    where T : class
{
    protected abstract IEnumerable<T> GetData();

    private IEnumerable<T[]> Data => GetData().Select(x => new[] { x });

    public IEnumerator<object[]> GetEnumerator() => Data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}