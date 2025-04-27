namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Services.Saga;

public interface ISagaInstance
{
    Guid CorrelationId { get; }
    string CurrentState { get; set; }
    string? ETag { get; set; }
    bool IsLocked { get; set; }
}