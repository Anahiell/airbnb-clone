using Airbnb.Connection.ConnectionService.MessageBusConnection.Configuration;

namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Interfaces;

public interface IOutboxStorage
{
    Task SaveAsync(object @event, string eventType, CancellationToken cancellationToken = default, string? stateLabel = null, string? deduplicationKey = null);
    Task<IReadOnlyList<OutboxMessage>> GetUndispatchedAsync(int take = 100, CancellationToken cancellationToken = default);
    Task MarkAsDispatchedAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
}