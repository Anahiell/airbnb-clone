using Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductReview;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Airbnb.ProductManagement.Application.BoundedContext.Consumers.ReviewConsumer;

public class ProductReviewDeletedHandlerStrategy : IEventHandlerStrategy
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProductReviewDeletedHandlerStrategy> _logger;

    public ProductReviewDeletedHandlerStrategy(IMediator mediator, ILogger<ProductReviewDeletedHandlerStrategy> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public bool CanHandle(INotification @event) => @event is ProductReviewDeletedEvent;

    public async Task HandleAsync(INotification @event, CancellationToken cancellationToken)
    {
        var deleted = (ProductReviewDeletedEvent)@event;

        _logger.LogInformation("Handling ProductReviewDeletedEvent for ReviewId={ReviewId}", deleted.Id);

        await _mediator.Publish(deleted, cancellationToken);
    }
}