namespace Airbnb.SharedKernel.ConnectionService.MessageBusConnection;

public class MessageBusOptions
{
    public MessageQueueType QueueType { get; set; }
    public string ConnectionString { get; set; } = string.Empty;
    public string ExchangeName { get; set; } = string.Empty;
    public string QueueName { get; set; } = string.Empty;
    public int RetryCount { get; set; } = 3;
}