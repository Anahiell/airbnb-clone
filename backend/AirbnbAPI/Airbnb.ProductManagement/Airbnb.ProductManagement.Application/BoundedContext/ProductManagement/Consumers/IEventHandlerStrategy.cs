using Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductOrder;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Airbnb.ProductManagement.Application.BoundedContext.Consumers;

public interface IEventHandlerStrategy
{
    bool CanHandle(INotification @event);
    Task HandleAsync(INotification @event, CancellationToken cancellationToken);
}

public class EventStrategyDispatcher
{
    private readonly IEnumerable<IEventHandlerStrategy> _strategies;
    private readonly ILogger<EventStrategyDispatcher> _logger;

    public EventStrategyDispatcher(IEnumerable<IEventHandlerStrategy> strategies, ILogger<EventStrategyDispatcher> logger)
    {
        _strategies = strategies;
        _logger = logger;
    }

    public async Task DispatchAsync(INotification @event, CancellationToken ct)
    {
        var strategy = _strategies.FirstOrDefault(s => s.CanHandle(@event));
        if (strategy == null)
        {
            _logger.LogWarning("No strategy found for event: {EventType}", @event.GetType().Name);
            return;
        }

        _logger.LogInformation("Dispatching {EventType} to strategy {Strategy}", @event.GetType().Name, strategy.GetType().Name);
        await strategy.HandleAsync(@event, ct);
    }
}

public class ProductEventConsumer<T> : IConsumer<T> where T : class, INotification
{
    private readonly EventStrategyDispatcher _dispatcher;

    public ProductEventConsumer(EventStrategyDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    public Task Consume(ConsumeContext<T> context)
        => _dispatcher.DispatchAsync(context.Message, context.CancellationToken);
}