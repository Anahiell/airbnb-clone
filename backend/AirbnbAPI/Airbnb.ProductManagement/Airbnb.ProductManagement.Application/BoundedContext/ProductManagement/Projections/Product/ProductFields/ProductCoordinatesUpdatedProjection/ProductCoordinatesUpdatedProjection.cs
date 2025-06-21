using Airbnb.Domain.BoundedContexts.ProductCoordinateManagement.Events;
using Airbnb.Domain.BoundedContexts.ProductCoordinateManagement.Interfaces;
using Airbnb.MongoRepository.Interfaces;
using Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;
using MediatR;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductManagement.Projections.Product.ProductCoordinatesUpdatedProjection;

public class ProductCoordinatesUpdatedEventHandler(IProjectionRepository<ProductEntityInfo> repository)
    : INotificationHandler<CoordinateUpdatedEvent>
{
    public async Task Handle(CoordinateUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var product = await repository.FindByIdAsync(notification.AggregateId, cancellationToken);
        if (product is null)
        {
            return;
        }

        product.Coordinates.Latitude = notification.Latitude;
        product.Coordinates.Longitude = notification.Longitude;

        await repository.UpdateAsync(product, cancellationToken);
    }
}