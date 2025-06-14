using Airbnb.MongoRepository.Interfaces;
using Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductReview;
using Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;
using MediatR;

namespace Airbnb.ProductManagement.Application.BoundedContext.Projections.ProductReviewsUpdatedProjection;

public class ProductReviewsUpdatedProjection : INotificationHandler<ProductReviewUpdatedEvent>
{
    private readonly IProjectionRepository<ProductEntityInfo> _productRepository;

    public ProductReviewsUpdatedProjection(IProjectionRepository<ProductEntityInfo> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Handle(ProductReviewUpdatedEvent @event, CancellationToken cancellationToken)
    {
        var product = await _productRepository.FindByIdAsync(@event.ProductId, cancellationToken);
        if (product == null)
        {
            return;
        }

        product.UpdateReview(new ReviewInfo
        {
            Id = @event.Id,
            ProductId = @event.ProductId,
            UserId = @event.UserId,
            Title = @event.Title,
            Description = @event.Description,
            Rating = @event.Rating,
            CreatedAt = @event.CreatedAt,
            UpdatedAt = @event.UpdatedAt
        });

        await _productRepository.UpdateAsync(product, cancellationToken);
    }
}