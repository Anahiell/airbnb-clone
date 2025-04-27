namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Configuration;

public class OutboxOptions
{
    public bool Enabled { get; set; }
    public string? StorageType { get; set; }
}