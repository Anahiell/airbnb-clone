using System.Text.Json;
using Airbnb.Connection.ConnectionService.MessageBusConnection.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Services;

public class OutboxDispatcher : BackgroundService
{
    private readonly IServiceProvider _provider;
    private readonly ILogger<OutboxDispatcher> _logger;

    public OutboxDispatcher(IServiceProvider provider, ILogger<OutboxDispatcher> logger)
    {
        _provider = provider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            using var scope = _provider.CreateScope();
            var outbox = scope.ServiceProvider.GetRequiredService<IOutboxStorage>();
            var sender = scope.ServiceProvider.GetRequiredService<IMessageTransportProvider>();

            var messages = await outbox.GetUndispatchedAsync(100, cancellationToken);
            foreach (var msg in messages)
            {
                var type = Type.GetType(msg.EventType);
                if (type is null)
                {
                    continue;
                }

                var @event = JsonSerializer.Deserialize(msg.Payload, type);
                if (@event is null)
                {
                    continue;
                }

                await sender.PublishAsync(@event, msg.EventType, msg.Exchange, cancellationToken: cancellationToken);
            }

            await outbox.MarkAsDispatchedAsync(messages.Select(m => m.Id), cancellationToken);

            await Task.Delay(5000, cancellationToken);
        }
    }
}