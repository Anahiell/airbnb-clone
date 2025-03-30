using Airbnb.SharedKernel.ConnectionService.MessageBusConnection;
using MassTransit;

namespace Airbnb.Connection.ConnectionService.MessageBusConnection;

public class RabbitMqMessageBusProvider : IMessageBusProvider
{
    private readonly IBusControl _bus;
    private readonly MessageBusOptions _options;

    public RabbitMqMessageBusProvider(MessageBusOptions options)
    {
        _options = options;
        _bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
        {
            cfg.Host(new Uri(_options.ConnectionString), h => { });
            cfg.ReceiveEndpoint(_options.QueueName, ep =>
            {
                ep.Consumer<Consumer>();
            });
        });
    }

    public async Task PublishAsync<T>(T message, CancellationToken cancellationToken = default)
    {
        await _bus.Publish(message, cancellationToken);
    }

    public async Task SubscribeAsync<T>(Func<T, Task> handler, CancellationToken cancellationToken = default)
        where T : class
    {
        _bus.ConnectReceiveEndpoint(_options.QueueName, cfg =>
        {
            cfg.Handler<T>(async context =>
            {
                await handler(context.Message);
            });
        });

        await _bus.StartAsync(cancellationToken);
    }
}