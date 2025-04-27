using Airbnb.Connection.ConnectionService.MessageBusConnection.Interfaces;
using Airbnb.Connection.ConnectionService.MessageBusConnection.Services.Middleware;
using Microsoft.Extensions.DependencyInjection;

namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Services.Saga;

public class SagaMiddleware : IMessageMiddleware
{
    private readonly IServiceProvider _provider;
    private readonly IEnumerable<ISagaFactory> _factories;

    public SagaMiddleware(IServiceProvider provider, IEnumerable<ISagaFactory> factories)
    {
        _provider = provider;
        _factories = factories;
    }

    public async Task InvokeAsync<T>(T message, Func<Task> next) where T : class
    {
        if (message is not ISagaMessage sagaMsg)
        {
            await next();
            return;
        }

        var factory = _factories.FirstOrDefault(f => f.CanHandle(typeof(T)));
        if (factory == null)
        {
            throw new InvalidOperationException($"No saga factory registered for message type {typeof(T).Name}");
        }

        using var scope = _provider.CreateScope();
        await factory.HandleAsync(sagaMsg.SagaId, message, scope.ServiceProvider);
    }
}

public interface ISagaMessage
{
    Guid SagaId { get; }
}