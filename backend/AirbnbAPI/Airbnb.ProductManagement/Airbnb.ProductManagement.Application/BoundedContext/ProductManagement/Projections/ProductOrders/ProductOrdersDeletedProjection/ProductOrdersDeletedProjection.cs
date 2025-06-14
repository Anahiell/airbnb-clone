using Airbnb.MongoRepository.Interfaces;
using Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductOrder;
using Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;
using MediatR;

namespace Airbnb.ProductManagement.Application.BoundedContext.Projections.ProductOrders.ProductOrdersDeletedProjection;

public class ProductOrdersDeletedProjection : INotificationHandler<ProductOrderDeletedEvent>
{
    private readonly IProjectionRepository<ProductEntityInfo> _productRepository;

    public ProductOrdersDeletedProjection(IProjectionRepository<ProductEntityInfo> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Handle(ProductOrderDeletedEvent @event, CancellationToken cancellationToken)
    {
        var product = await _productRepository.FindByIdAsync(@event.ProductId, cancellationToken);
        if (product == null)
        {
            return;
        }

        product.RemoveOrder();

        await _productRepository.UpdateAsync(product, cancellationToken);
    }
}