using Airbnb.Domain.BoundedContexts.ProductFacilityManagement.ProductFacility.Events;
using Airbnb.MongoRepository.Interfaces;
using Airbnb.ProductManagement.Application.BoundedContext.ProductFacilityManagement.QueryObjects;
using Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;
using MediatR;
namespace Airbnb.ProductManagement.Application.BoundedContext.ProductManagement.Projections.Product.ProductFields.ProductFacilitiesUpdatedProjection;

public class ProductFacilitiesUpdatedEventHandler(IProjectionRepository<ProductEntityInfo> repository)
    : INotificationHandler<ProductFacilitiesUpdatedEvent>
{
    public async Task Handle(ProductFacilitiesUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var product = await repository.FindByIdAsync(notification.ProductId, cancellationToken);
        if (product is null) return;

        product.Facilities = new List<FacilityEntityInfo>();

        foreach (var facility in notification.Facilities)
        {
            product.Facilities.Add(new FacilityEntityInfo
            {
                Id = facility.Id,
                Name = facility.Name,
                IconName = facility.IconName,
            });
        }

        await repository.UpdateAsync(product, cancellationToken);
    }
}