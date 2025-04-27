using Airbnb.Connection.ConnectionService.MessageBusConnection.Services.Middleware;

namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Services.Filter;

public class FilterMiddleware : IMessageMiddleware
{
    private readonly IEnumerable<IMessageFilter> _filters;

    public FilterMiddleware(IEnumerable<IMessageFilter> filters)
    {
        _filters = filters;
    }

    public async Task InvokeAsync<T>(T message, Func<Task> next) where T : class
    {
        foreach (var filter in _filters)
        {
            if (!await filter.ShouldProcessAsync(message))
                return;
        }

        await next();
    }
}