namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Services.Saga;

public interface ISaga
{
    Guid Id { get; }
    string State { get; }
    bool IsCompleted { get; }
    DateTime? ExpireAt { get; }
    Task HandleAsync<T>(T message, CancellationToken cancellationToken = default) where T : class;
    Task CompensateAsync(CancellationToken cancellationToken = default);
}