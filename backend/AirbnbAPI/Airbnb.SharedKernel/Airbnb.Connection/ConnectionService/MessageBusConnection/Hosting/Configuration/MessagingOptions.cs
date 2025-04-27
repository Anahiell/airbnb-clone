namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Configuration;

public class MessagingOptions
{
    public TransportType TransportType { get; set; }
    public string? Host { get; set; }
    public string? QueueName { get; set; }
    public RetryPolicyOptions RetryPolicy { get; set; } = new();
    public OutboxOptions Outbox { get; set; } = new();
    public KafkaOptions KafkaOptions { get; set; } = new();
    public RabbitMqOptions RabbitMqOptions { get; set; } = new();
}