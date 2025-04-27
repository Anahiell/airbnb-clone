namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Configuration;

public class KafkaOptions
{
    public string? BootstrapServers { get; set; }
    public string? Topic { get; set; }
}
