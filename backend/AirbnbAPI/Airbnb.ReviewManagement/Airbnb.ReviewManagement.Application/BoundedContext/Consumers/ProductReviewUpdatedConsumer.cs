using Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductReview;
using Airbnb.ReviewManagement.Domain.BoundedContexts.ReviewManagement.Aggregates;
using Airbnb.ReviewManagement.Domain.BoundedContexts.ReviewManagement.Events;
using Airbnb.SharedKernel.Repositories;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Airbnb.ReviewManagement.Application.BoundedContext.Consumers;

public class ProductReviewUpdatedConsumer : IConsumer<ProductReviewUpdatedEvent>
{
    private readonly IRepository<DomainReview> _reviewRepository;
    private readonly ILogger<ProductReviewUpdatedConsumer> _logger;
    private readonly IMediator _mediator;

    public ProductReviewUpdatedConsumer(
        IRepository<DomainReview> reviewRepository,
        ILogger<ProductReviewUpdatedConsumer> logger,
        IMediator mediator)
    {
        _reviewRepository = reviewRepository;
        _logger = logger;
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<ProductReviewUpdatedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Processing ProductReviewUpdatedEvent...");

        try
        {
            var review = new DomainReview(message.Title, message.Description, message.Rating, DateTime.Now, message.UserId, message.ProductId);

            await _reviewRepository.UpdateAsync(review, context.CancellationToken);

            await _mediator.Publish(
                new ReviewUpdatedEvent(review.Id, message.Title, message.Description, message.Rating, DateTime.Now,
                    message.UserId, message.ProductId), context.CancellationToken);

            _logger.LogInformation($"Processed review update for ProductId: {message.ProductId}, ReviewId: {message.Id}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while processing ProductReviewUpdatedEvent.");
            throw;
        }
    }
}