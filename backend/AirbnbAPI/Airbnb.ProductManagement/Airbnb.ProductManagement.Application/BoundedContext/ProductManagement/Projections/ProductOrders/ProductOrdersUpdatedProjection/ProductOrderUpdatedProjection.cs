using Airbnb.MongoRepository.Interfaces;
using Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductOrder;
using Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;
using MediatR;

namespace Airbnb.ProductManagement.Application.BoundedContext.Projections.ProductOrdersUpdatedProjection;

public class ProductOrderUpdatedProjection : INotificationHandler<ProductOrderUpdatedEvent>
{
    private readonly IProjectionRepository<ProductEntityInfo> _productRepository;

    public ProductOrderUpdatedProjection(IProjectionRepository<ProductEntityInfo> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Handle(ProductOrderUpdatedEvent @event, CancellationToken cancellationToken)
    {
        var product = await _productRepository.FindByIdAsync(@event.ProductId, cancellationToken);
        if (product == null)
        {
            return;
        }

        product.UpdateOrder(new OrderInfo
        {
            Id = @event.OrderId,
            ProductId = @event.ProductId,
            UserId = @event.UserId,
            DateStart = @event.DateStart,
            DateEnd = @event.DateEnd
        });

        await _productRepository.UpdateAsync(product, cancellationToken);
    }
}