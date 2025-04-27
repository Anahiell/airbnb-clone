using Microsoft.Extensions.Logging;

namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Services.Middleware;

public class TracingMiddleware : IMessageMiddleware
{
    private readonly ILogger<TracingMiddleware> _logger;

    public TracingMiddleware(ILogger<TracingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync<T>(T message, Func<Task> next) where T : class
    {
        var traceId = Guid.NewGuid().ToString();
        _logger.LogInformation($"TraceId: {traceId} - Handling message of type {typeof(T).Name}");

        await next();

        _logger.LogInformation($"TraceId: {traceId} - Finished handling message of type {typeof(T).Name}");
    }
}