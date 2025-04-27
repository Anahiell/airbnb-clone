namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Services.Saga;

public class SagaManager
{
    private readonly ISagaStore _store;

    public SagaManager(ISagaStore store)
    {
        _store = store;
    }

    public async Task HandleAsync<T>(Guid sagaId, T message, Func<Guid, ISaga> sagaFactory, CancellationToken cancellationToken = default) where T : class
    {
        var saga = await _store.GetAsync(sagaId, cancellationToken) ?? sagaFactory(sagaId);
        await saga.HandleAsync(message, cancellationToken);

        if (saga.IsCompleted)
        {
            await _store.CompleteAsync(sagaId, cancellationToken);
        }
        else
        {
            await _store.SaveAsync(saga, cancellationToken);
        }
    }
}
