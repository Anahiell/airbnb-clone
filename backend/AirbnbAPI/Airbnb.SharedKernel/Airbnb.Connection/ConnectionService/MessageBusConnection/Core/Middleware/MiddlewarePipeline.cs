namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Services.Middleware;

public class MiddlewarePipeline
{
    private readonly List<Func<IMessageMiddleware>> _middlewares;

    public MiddlewarePipeline(IEnumerable<Func<IMessageMiddleware>> middlewares)
    {
        _middlewares = middlewares.ToList();
    }

    public async Task ExecuteAsync<T>(T message, Func<Task> final) where T : class
    {
        var enumerator = _middlewares.Select(m => m()).Reverse().Aggregate(final, (next, middleware) =>
            () => middleware.InvokeAsync(message, next));

        await enumerator();
    }
}