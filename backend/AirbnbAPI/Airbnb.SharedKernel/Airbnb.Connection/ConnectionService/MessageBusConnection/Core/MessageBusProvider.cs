using Airbnb.Connection.ConnectionService.MessageBusConnection.Configuration;
using Airbnb.Connection.ConnectionService.MessageBusConnection.Interfaces;

namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Services;

public class MessageBusProvider : IMessageBusProvider
{
    private readonly IMessageTransportProvider _internalProvider;

    public MessageBusProvider(MessagingOptions options, IMessageTransportProviderFactory factory)
    {
        _internalProvider = factory.Create(options);
    }

    public async Task StartAsync(CancellationToken cancellationToken = default) =>
        await _internalProvider.StartAsync(cancellationToken);

    public async Task StopAsync(CancellationToken cancellationToken = default) =>
        await _internalProvider.StopAsync(cancellationToken);

    public Task PublishAsync<T>(T message, string queueName, CancellationToken cancellationToken = default) where T : class =>
        _internalProvider.PublishAsync(message, queueName, cancellationToken: cancellationToken);

    public Task SubscribeAsync<T>(string queueName, Func<T, Task> handler, CancellationToken cancellationToken = default) where T : class =>
        _internalProvider.SubscribeAsync(queueName, handler, cancellationToken);
}
