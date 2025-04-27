using Microsoft.Extensions.Logging;

namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Services.Middleware;

public class ErrorHandlingMiddleware : IMessageMiddleware
{
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync<T>(T message, Func<Task> next) where T : class
    {
        try
        {
            await next();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error processing message of type {typeof(T).Name}");
            throw; // возможно, сюда внедрять retry или Dead Letter
        }
    }
}