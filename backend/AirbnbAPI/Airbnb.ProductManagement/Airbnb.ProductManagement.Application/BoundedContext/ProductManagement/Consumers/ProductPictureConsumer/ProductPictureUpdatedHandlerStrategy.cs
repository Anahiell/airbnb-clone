using Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductPicture;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Airbnb.ProductManagement.Application.BoundedContext.Consumers.ProductPictureConsumer;

public class ProductPictureUpdatedHandlerStrategy : IEventHandlerStrategy
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProductPictureUpdatedHandlerStrategy> _logger;

    public ProductPictureUpdatedHandlerStrategy(IMediator mediator, ILogger<ProductPictureUpdatedHandlerStrategy> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public bool CanHandle(INotification @event) => @event is ProductPictureUpdatedEvent;

    public async Task HandleAsync(INotification @event, CancellationToken cancellationToken)
    {
        var updated = (ProductPictureUpdatedEvent)@event;

        _logger.LogInformation("Handling ProductPictureUpdatedEvent for ProductId={ProductId}, PictureId={PictureId}", updated.ProductId, updated.PictureId);

        await _mediator.Publish(updated, cancellationToken);

    }
}