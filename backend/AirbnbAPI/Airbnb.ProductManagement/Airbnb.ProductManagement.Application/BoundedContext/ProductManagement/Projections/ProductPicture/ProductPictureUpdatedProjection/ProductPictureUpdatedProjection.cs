using Airbnb.MongoRepository.Interfaces;
using Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductPicture;
using Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;
using MediatR;

namespace Airbnb.ProductManagement.Application.BoundedContext.Projections.ProductPictureUpdatedProjection;

public class ProductPictureUpdatedProjection : INotificationHandler<ProductPictureUpdatedEvent>
{
    private readonly IProjectionRepository<ProductEntityInfo> _productRepository;

    public ProductPictureUpdatedProjection(IProjectionRepository<ProductEntityInfo> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Handle(ProductPictureUpdatedEvent @event, CancellationToken cancellationToken)
    {
        var product = await _productRepository.FindByIdAsync(@event.ProductId, cancellationToken);
        if (product == null)
        {
            return;
        }

        product.UpdatePicture(new PictureInfo
        {
            Id = @event.PictureId,
            Url = @event.Url,
            ProductId = @event.ProductId
        });

        await _productRepository.UpdateAsync(product, cancellationToken);
    }
}