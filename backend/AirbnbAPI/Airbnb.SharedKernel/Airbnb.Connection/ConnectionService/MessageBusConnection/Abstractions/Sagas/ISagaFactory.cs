namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Services.Saga;

public interface ISagaFactory
{
    bool CanHandle(Type messageType);
    Task HandleAsync(Guid sagaId, object message, IServiceProvider provider, CancellationToken cancellationToken = default);
}