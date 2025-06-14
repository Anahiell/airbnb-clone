using Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductTag;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Airbnb.ProductManagement.Application.BoundedContext.Consumers.TagConsumer;


public class ProductTagDeletedHandlerStrategy : IEventHandlerStrategy
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProductTagDeletedHandlerStrategy> _logger;

    public ProductTagDeletedHandlerStrategy(IMediator mediator, ILogger<ProductTagDeletedHandlerStrategy> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public bool CanHandle(INotification @event) => @event is ProductTagDeletedEvent;

    public async Task HandleAsync(INotification @event, CancellationToken cancellationToken)
    {
        var deleted = (ProductTagDeletedEvent)@event;
        _logger.LogInformation("Handling ProductTagDeletedEvent: ProductId={ProductId}, TagId={TagId}", deleted.ProductId, deleted.TagId);
        await _mediator.Publish(deleted, cancellationToken);
    }
}