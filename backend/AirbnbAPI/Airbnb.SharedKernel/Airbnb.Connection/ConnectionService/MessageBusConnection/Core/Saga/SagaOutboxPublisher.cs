using Airbnb.Connection.ConnectionService.MessageBusConnection.Interfaces;
using Polly;

namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Services.Saga;

public class SagaOutboxPublisher
{
    private readonly IOutboxStorage _outbox;
    private readonly IMessageBusProvider _sender;
    private readonly IAsyncPolicy _retry;

    public SagaOutboxPublisher(IOutboxStorage outbox, IMessageBusProvider sender, IAsyncPolicy retry)
    {
        _outbox = outbox;
        _sender = sender;
        _retry = retry;
    }

    public async Task PublishAsync<T>(T @event, string label = null, CancellationToken ct = default) where T : class
    {
        var eventType = @event.GetType().AssemblyQualifiedName!;
        await _outbox.SaveAsync(@event, eventType, ct, stateLabel: label);

        // retry publishing
        await _retry.ExecuteAsync(async () =>
        {
            await _sender.PublishAsync(@event, eventType, ct);
        });
    }
}