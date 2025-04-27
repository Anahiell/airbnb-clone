using Microsoft.Extensions.Logging;

namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Services.DeadLetterQueue;

public class LoggingDeadLetterQueue : IDeadLetterQueue
{
    private readonly ILogger<LoggingDeadLetterQueue> _logger;

    public LoggingDeadLetterQueue(ILogger<LoggingDeadLetterQueue> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(string messageType, string payload, Exception exception)
    {
        _logger.LogError(exception, "DLQ: Failed to handle message {Type}. Payload: {Payload}", messageType, payload);
        return Task.CompletedTask;
    }
}