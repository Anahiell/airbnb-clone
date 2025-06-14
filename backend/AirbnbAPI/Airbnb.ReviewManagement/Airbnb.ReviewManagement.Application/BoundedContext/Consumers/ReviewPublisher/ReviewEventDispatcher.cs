
using Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent;
using Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductReview;
using Airbnb.ProductManagement.Application.BoundedContext.ProductReviewUpdatedConsumer.ReviewConsumer;
using Airbnb.SharedKernel.Repositories;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductReviewUpdatedConsumer;

public class ReviewEventDispatcher : IReviewEventDispatcher
{
    private readonly IMediator _mediator;
    private readonly ILogger<ReviewEventDispatcher> _logger;
    private readonly IBus _bus;

    public ReviewEventDispatcher(IMediator mediator, ILogger<ReviewEventDispatcher> logger, IBus bus)
    {
        _mediator = mediator;
        _logger = logger;
        _bus = bus;
    }

    public async Task DispatchAsync(IReviewEvent evt, CancellationToken ct)
    {
        _logger.LogInformation("Handling {EventType} for ProductId={ProductId}", evt.GetType().Name, evt.ProductId);

        switch (evt)
        {
            case ProductReviewCreatedEvent created:
                _logger.LogInformation("→ Review Created: {ReviewId}", created.Id);
                await _bus.Publish(created, ct);
                break;

            case ProductReviewUpdatedEvent updated:
                _logger.LogInformation("→ Review Updated: {ReviewId} by UserId={UserId}", updated.Id, updated.UserId);
                await _bus.Publish(updated, ct);
                break;

            case ProductReviewDeletedEvent deleted:
                _logger.LogInformation("→ Review Deleted: {ReviewId}", deleted.Id);
                await _bus.Publish(deleted, ct);
                break;
        }

        // Дополнительно можно бросить общее ProductUpdatedEvent, если нужно
        // await _mediator.Publish(new ProductUpdatedEvent(updated.ProductId, ...), ct);
    }
}
