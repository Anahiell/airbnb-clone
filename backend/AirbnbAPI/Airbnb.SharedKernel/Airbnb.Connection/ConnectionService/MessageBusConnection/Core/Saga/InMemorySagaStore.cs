namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Services.Saga;

public class InMemorySagaStore : ISagaStore
{
    private readonly Dictionary<Guid, ISaga> _sagas = new();

    public Task<ISaga?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        _sagas.TryGetValue(id, out var saga);
        return Task.FromResult(saga);
    }

    public Task SaveAsync(ISaga saga, CancellationToken cancellationToken = default)
    {
        _sagas[saga.Id] = saga;
        return Task.CompletedTask;
    }

    public Task CompleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        _sagas.Remove(id);
        return Task.CompletedTask;
    }
}
