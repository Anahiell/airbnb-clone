using Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent;
using Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductPicture;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Airbnb.ProductManagement.Application.BoundedContext.Consumers.ProductPictureConsumer;

public class ProductPictureDeletedHandlerStrategy : IEventHandlerStrategy
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProductPictureDeletedHandlerStrategy> _logger;

    public ProductPictureDeletedHandlerStrategy(IMediator mediator, ILogger<ProductPictureDeletedHandlerStrategy> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public bool CanHandle(INotification @event) => @event is ProductPictureDeletedEvent;

    public async Task HandleAsync(INotification @event, CancellationToken cancellationToken)
    {
        var deleted = (ProductPictureDeletedEvent)@event;

        _logger.LogInformation("Handling ProductPictureDeletedEvent for ProductId={ProductId}, PictureId={PictureId}", deleted.ProductId, deleted.PictureId);

        /*
        await _mediator.Publish(new ProductUpdatedEvent(deleted.ProductId), cancellationToken);
        */
    }
}