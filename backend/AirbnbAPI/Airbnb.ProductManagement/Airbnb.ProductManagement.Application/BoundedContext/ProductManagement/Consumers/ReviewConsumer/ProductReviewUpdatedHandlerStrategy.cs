using Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductReview;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Airbnb.ProductManagement.Application.BoundedContext.Consumers.ReviewConsumer;

public class ProductReviewUpdatedHandlerStrategy : IEventHandlerStrategy
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProductReviewUpdatedHandlerStrategy> _logger;

    public ProductReviewUpdatedHandlerStrategy(IMediator mediator, ILogger<ProductReviewUpdatedHandlerStrategy> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public bool CanHandle(INotification @event) => @event is ProductReviewUpdatedEvent;

    public async Task HandleAsync(INotification @event, CancellationToken cancellationToken)
    {
        var updated = (ProductReviewUpdatedEvent)@event;
        _logger.LogInformation("Handling ProductReviewUpdatedEvent for ReviewId={ReviewId}", updated.Id);
        await _mediator.Publish(updated, cancellationToken);
    }
}