using Airbnb.MongoRepository.Interfaces;
using Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductPicture;
using Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductReview;
using Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;
using MediatR;

namespace Airbnb.ProductManagement.Application.BoundedContext.Projections.ProductPicture.ProductPictureDeletedProjection;

public class ProductPictureDeletedProjection : INotificationHandler<ProductPictureDeletedEvent>
{
    private readonly IProjectionRepository<ProductEntityInfo> _productRepository;

    public ProductPictureDeletedProjection(IProjectionRepository<ProductEntityInfo> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Handle(ProductPictureDeletedEvent @event, CancellationToken cancellationToken)
    {
        var product = await _productRepository.FindByIdAsync(@event.ProductId, cancellationToken);
        if (product == null)
        {
            return;
        }

        product.RemovePictures();

        await _productRepository.UpdateAsync(product, cancellationToken);
    }
}