namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Services.Saga;

public interface ISagaIdempotencyService
{
    Task<bool> AcquireLockAsync(Guid sagaId, string stepLabel);
    Task ReleaseLockAsync(Guid sagaId, string stepLabel);

    Task<bool> IsHandledAsync(Guid sagaId, string stepLabel, string? etag = null);
    Task MarkHandledAsync(Guid sagaId, string stepLabel, string? etag = null);
}