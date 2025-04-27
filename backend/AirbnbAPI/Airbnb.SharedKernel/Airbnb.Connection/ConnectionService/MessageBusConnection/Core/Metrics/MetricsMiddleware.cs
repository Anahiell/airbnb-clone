using System.Diagnostics;
using Airbnb.Connection.ConnectionService.MessageBusConnection.Services.Middleware;

namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Services.Metrics;

public class MetricsMiddleware : IMessageMiddleware
{
    private readonly IMessageMetrics _metrics;

    public MetricsMiddleware(IMessageMetrics metrics)
    {
        _metrics = metrics;
    }

    public async Task InvokeAsync<T>(T message, Func<Task> next) where T : class
    {
        var sw = Stopwatch.StartNew();
        _metrics.MessageReceived(typeof(T).Name);

        try
        {
            await next();
            _metrics.MessageHandled(typeof(T).Name, sw.Elapsed);
        }
        catch
        {
            _metrics.MessageFailed(typeof(T).Name);
            throw;
        }
    }
}
