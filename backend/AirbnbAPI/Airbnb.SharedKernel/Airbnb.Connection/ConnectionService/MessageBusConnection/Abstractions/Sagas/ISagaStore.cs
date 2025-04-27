namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Services.Saga;

public interface ISagaStore
{
    Task<ISaga?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task SaveAsync(ISaga saga, CancellationToken cancellationToken = default);
    Task CompleteAsync(Guid id, CancellationToken cancellationToken = default);
}
