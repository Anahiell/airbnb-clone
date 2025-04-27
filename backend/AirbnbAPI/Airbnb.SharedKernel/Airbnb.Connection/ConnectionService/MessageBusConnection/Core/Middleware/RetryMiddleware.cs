using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;

namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Services.Middleware;

public class RetryMiddleware : IMessageMiddleware
{
    private readonly ILogger<RetryMiddleware> _logger;
    private readonly AsyncRetryPolicy _retryPolicy;

    public RetryMiddleware(ILogger<RetryMiddleware> logger, int retryCount = 3)
    {
        _logger = logger;
        _retryPolicy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(retryCount,
                attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
                (exception, timeSpan, attempt, context) =>
                {
                    _logger.LogWarning(exception, "Retry {Attempt} after {Delay}s", attempt, timeSpan.TotalSeconds);
                });
    }

    public async Task InvokeAsync<T>(T message, Func<Task> next) where T : class
    {
        await _retryPolicy.ExecuteAsync(() => next());
    }
}