using Microsoft.Extensions.DependencyInjection;

namespace Airbnb.Connection.ConnectionService.MessageBusConnection.Services.Saga;

public class SagaFactory<TMessage, TSaga> : ISagaFactory
    where TMessage : class, ISagaMessage
    where TSaga : ISaga
{
    private readonly Func<Guid, IServiceProvider, TSaga> _factory;

    public SagaFactory(Func<Guid, IServiceProvider, TSaga> factory)
    {
        _factory = factory;
    }

    public bool CanHandle(Type messageType) => typeof(TMessage) == messageType;

    public async Task HandleAsync(Guid sagaId, object message, IServiceProvider provider, CancellationToken cancellationToken = default)
    {
        var manager = provider.GetRequiredService<SagaManager>();
        var saga = _factory(sagaId, provider);
        await manager.HandleAsync(sagaId, (TMessage)message, _ => saga, cancellationToken);
    }
}
