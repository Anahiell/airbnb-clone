using Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductReview;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Airbnb.ProductManagement.Application.BoundedContext.Consumers.ReviewConsumer;

public class ProductReviewCreatedHandlerStrategy : IEventHandlerStrategy
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProductReviewCreatedHandlerStrategy> _logger;

    public ProductReviewCreatedHandlerStrategy(IMediator mediator, ILogger<ProductReviewCreatedHandlerStrategy> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public bool CanHandle(INotification @event) => @event is ProductReviewCreatedEvent;

    public Task HandleAsync(INotification @event, CancellationToken cancellationToken)
    {
        var created = (ProductReviewCreatedEvent)@event;
        _logger.LogInformation("→ Review Created Strategy: {ReviewId}", created.Id);
        return _mediator.Publish(created, cancellationToken);
    }
}