using System.Text.Json;
using Airbnb.Connection.ConnectionService.MessageBusConnection.Configuration;
using Airbnb.Connection.ConnectionService.MessageBusConnection.Interfaces;

namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Services;

public class InMemoryOutboxStorage : IOutboxStorage
{
    private readonly List<OutboxMessage> _messages = new();

    public Task SaveAsync(object @event, string eventType, CancellationToken ct = default, string? stateLabel = null, string? deduplicationKey = null)
    {
        if (deduplicationKey != null && _messages.Any(m => m.DeduplicationKey == deduplicationKey))
            return Task.CompletedTask;

        var message = new OutboxMessage
        {
            EventType = eventType,
            Payload = JsonSerializer.Serialize(@event),
            StateLabel = stateLabel,
            DeduplicationKey = deduplicationKey
        };
        _messages.Add(message);
        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<OutboxMessage>> GetUndispatchedAsync(int take = 100, CancellationToken ct = default)
        => Task.FromResult(_messages.Where(m => !m.IsDispatched).Take(take).ToList() as IReadOnlyList<OutboxMessage>);

    public Task MarkAsDispatchedAsync(IEnumerable<Guid> ids, CancellationToken ct = default)
    {
        foreach (var msg in _messages.Where(m => ids.Contains(m.Id)))
        {
            msg.IsDispatched = true;
            msg.DispatchedAt = DateTime.UtcNow;
        }
        return Task.CompletedTask;
    }
}