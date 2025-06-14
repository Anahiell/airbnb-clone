using Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent;
using Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductPicture;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Airbnb.ProductManagement.Application.BoundedContext.Consumers.ProductPictureConsumer;

public class ProductPictureCreatedHandlerStrategy : IEventHandlerStrategy
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProductPictureCreatedHandlerStrategy> _logger;

    public ProductPictureCreatedHandlerStrategy(IMediator mediator, ILogger<ProductPictureCreatedHandlerStrategy> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public bool CanHandle(INotification @event) => @event is ProductPictureCreatedEvent;

    public async Task HandleAsync(INotification @event, CancellationToken cancellationToken)
    {
        var evt = (ProductPictureCreatedEvent)@event;
        _logger.LogInformation("Handling ProductPictureCreatedEvent for ProductId={ProductId}, PictureId={PictureId}", evt.ProductId, evt.PictureId);
        await _mediator.Publish(evt, cancellationToken);
    }
}