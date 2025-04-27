namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Configuration;

public class OutboxMessage
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Payload { get; set; }
    public string QueueName { get; set; }
    public string? Exchange { get; set; }
    public bool Published { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public string EventType { get; set; } = null!;
    public string? StateLabel { get; set; }
    public bool IsDispatched { get; set; }
    public DateTime? DispatchedAt { get; set; }
    public string? DeduplicationKey { get; set; }
}