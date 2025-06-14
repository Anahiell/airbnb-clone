using Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductReview;
using Airbnb.ReviewManagement.Domain.BoundedContexts.ReviewManagement.Aggregates;
using Airbnb.ReviewManagement.Domain.BoundedContexts.ReviewManagement.Interfaces;
using Airbnb.SharedKernel.Repositories;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Airbnb.ReviewManagement.Application.BoundedContext.Consumers;

public class ProductReviewDeletedConsumer : IConsumer<ProductReviewDeletedEvent>
{
    private readonly IReviewRepository _reviewRepository;
    private readonly ILogger<ProductReviewDeletedConsumer> _logger;

    public ProductReviewDeletedConsumer(
        IReviewRepository reviewRepository,
        ILogger<ProductReviewDeletedConsumer> logger)
    {
        _reviewRepository = reviewRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ProductReviewDeletedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation($"Processing ProductDeletedEvent for ProductId: {message.ProductId}");

        try
        {
            await _reviewRepository.DeleteWhereAsync(r => r.ProductId == message.ProductId, context.CancellationToken);

            _logger.LogInformation($"Successfully deleted reviews for ProductId: {message.ProductId}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting reviews for ProductId: {message.ProductId}");
            throw;
        }
    }
}