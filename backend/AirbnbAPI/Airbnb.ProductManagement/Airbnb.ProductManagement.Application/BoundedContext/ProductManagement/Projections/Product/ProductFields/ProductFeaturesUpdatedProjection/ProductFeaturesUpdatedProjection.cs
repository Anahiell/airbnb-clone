using Airbnb.Domain.BoundedContexts.ProductFeatureManagement.Feature.Events;
using Airbnb.Domain.BoundedContexts.ProductFeatureManagement.ProductFeature.Events;
using Airbnb.MongoRepository.Interfaces;
using Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;
using MediatR;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductManagement.Projections.Product.ProductFields.ProductFeaturesUpdatedProjection;

public class ProductFeaturesUpdatedEventHandler(IProjectionRepository<ProductEntityInfo> repository)
    : INotificationHandler<ProductFeaturesUpdatedEvent>
{
    public async Task Handle(ProductFeaturesUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var product = await repository.FindByIdAsync(notification.AggregateId, cancellationToken);
        if (product is null)
        {
            return;
        }

        product.Features = new List<FeatureEntityInfo>();

        foreach (var facility in notification.FeatureNames)
        {
            product.Features.Add(new FeatureEntityInfo
            {
                Id = facility.Id,
                Name = facility.Name,
                Forcibly = facility.Forcibility,
                Price = facility.Price,
            });
        }

        await repository.UpdateAsync(product, cancellationToken);
    }
}