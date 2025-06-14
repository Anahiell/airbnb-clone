using Airbnb.MongoRepository.Interfaces;
using Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductReview;
using Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;
using MediatR;

namespace Airbnb.ProductManagement.Application.BoundedContext.Projections.ProductReview.ProductReviewsDeletedProjection;

public class ProductReviewsDeletedProjection : INotificationHandler<ProductReviewDeletedEvent>
{
    private readonly IProjectionRepository<ProductEntityInfo> _productRepository;

    public ProductReviewsDeletedProjection(IProjectionRepository<ProductEntityInfo> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Handle(ProductReviewDeletedEvent @event, CancellationToken cancellationToken)
    {
        var product = await _productRepository.FindByIdAsync(@event.ProductId, cancellationToken);
        if (product == null)
        {
            return;
        }

        product.RemoveReviews();

        await _productRepository.UpdateAsync(product, cancellationToken);
    }
}