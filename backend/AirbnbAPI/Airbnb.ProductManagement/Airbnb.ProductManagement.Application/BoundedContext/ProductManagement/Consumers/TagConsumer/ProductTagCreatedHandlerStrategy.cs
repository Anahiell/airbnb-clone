using Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductTag;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Airbnb.ProductManagement.Application.BoundedContext.Consumers.TagConsumer;

public class ProductTagCreatedHandlerStrategy : IEventHandlerStrategy
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProductTagCreatedHandlerStrategy> _logger;

    public ProductTagCreatedHandlerStrategy(IMediator mediator, ILogger<ProductTagCreatedHandlerStrategy> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public bool CanHandle(INotification @event) => @event is ProductTagCreatedEvent;

    public Task HandleAsync(INotification @event, CancellationToken cancellationToken)
    {
        var created = (ProductTagCreatedEvent)@event;
        _logger.LogInformation("→ Tag Created Strategy: ProductId={ProductId}, TagId={TagId}", created.ProductId, created.TagId);
        return _mediator.Publish(created, cancellationToken);
    }
}