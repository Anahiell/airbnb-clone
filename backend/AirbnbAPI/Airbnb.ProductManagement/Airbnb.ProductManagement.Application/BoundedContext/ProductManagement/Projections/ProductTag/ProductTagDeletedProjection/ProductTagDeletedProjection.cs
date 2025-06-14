using Airbnb.MongoRepository.Interfaces;
using Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductTag;
using Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;
using MediatR;

namespace Airbnb.ProductManagement.Application.BoundedContext.Projections.ProductTag.ProductTagDeletedProjection;

public class ProductTagDeletedProjection : INotificationHandler<ProductTagDeletedEvent>
{
    private readonly IProjectionRepository<ProductEntityInfo> _productRepository;

    public ProductTagDeletedProjection(IProjectionRepository<ProductEntityInfo> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Handle(ProductTagDeletedEvent @event, CancellationToken cancellationToken)
    {
        var product = await _productRepository.FindByIdAsync(@event.ProductId, cancellationToken);

        if (product == null)
        {
            return;
        }

        product.RemoveTags();

        await _productRepository.UpdateAsync(product, cancellationToken);
    }
}