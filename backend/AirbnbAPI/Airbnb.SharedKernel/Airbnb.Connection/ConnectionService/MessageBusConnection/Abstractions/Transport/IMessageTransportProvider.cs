namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Interfaces;

public interface IMessageTransportProvider
{
    Task PublishAsync<T>(T message, string queueName, string? exchange = null, CancellationToken cancellationToken = default) where T : class;
    Task SubscribeAsync<T>(string queueName, Func<T, Task> handler, CancellationToken cancellationToken = default) where T : class;
    Task StartAsync(CancellationToken cancellationToken = default);
    Task StopAsync(CancellationToken cancellationToken = default);
}