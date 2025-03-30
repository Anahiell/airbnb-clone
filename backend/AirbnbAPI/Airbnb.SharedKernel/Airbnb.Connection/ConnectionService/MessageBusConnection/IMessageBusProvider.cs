namespace Airbnb.SharedKernel.ConnectionService.MessageBusConnection;

public interface IMessageBusProvider
{
    Task PublishAsync<T>(T message, CancellationToken cancellationToken = default);
    Task SubscribeAsync<T>(Func<T, Task> handler, CancellationToken cancellationToken = default) where T : class;
}