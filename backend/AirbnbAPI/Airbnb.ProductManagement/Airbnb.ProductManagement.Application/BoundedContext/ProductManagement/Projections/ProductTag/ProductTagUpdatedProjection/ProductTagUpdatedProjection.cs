using Airbnb.MongoRepository.Interfaces;
using Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductTag;
using Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;
using MediatR;

namespace Airbnb.ProductManagement.Application.BoundedContext.Projections.ProductTagUpdatedProjection;

public class ProductTagsUpdatedProjection : INotificationHandler<ProductTagUpdatedEvent>
{
    private readonly IProjectionRepository<ProductEntityInfo> _productRepository;

    public ProductTagsUpdatedProjection(IProjectionRepository<ProductEntityInfo> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Handle(ProductTagUpdatedEvent @event, CancellationToken cancellationToken)
    {
        var product = await _productRepository.FindByIdAsync(@event.ProductId, cancellationToken);

        if (product == null)
        {
            return;
        }

        product.UpdateTag(new TagInfo
        {
            Id = @event.TagId,
            TagName = @event.TagName
        });

        await _productRepository.UpdateAsync(product, cancellationToken);
    }
}