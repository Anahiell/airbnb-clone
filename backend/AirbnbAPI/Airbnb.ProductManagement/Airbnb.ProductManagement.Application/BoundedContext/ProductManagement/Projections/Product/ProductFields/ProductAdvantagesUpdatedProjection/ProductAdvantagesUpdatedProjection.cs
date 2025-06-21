using Airbnb.Domain.BoundedContexts.ProductAdvantageManagement.Events;
using Airbnb.MongoRepository.Interfaces;
using Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;
using MediatR;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductManagement.Projections.Product.ProductFields.ProductAdvantagesUpdatedProjection;

public class ProductAdvantagesUpdatedProjection(IProjectionRepository<ProductEntityInfo> repository)
    : INotificationHandler<ProductAdvantagesUpdatedEvent>
{
    public async Task Handle(ProductAdvantagesUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var product = await repository.FindByIdAsync(notification.AggregateId, cancellationToken);
        if (product is null)
        {
            return;
        }

        var advantagesList = new List<AdvantagesEntityInfo>();
        foreach (var advantage in notification.Advantages)
        {
            advantagesList.Add(new AdvantagesEntityInfo
            {
                Id = advantage.Id,
                Title = advantage.Title,
                Description = advantage.Description,
            });
        }
        product.Advantages = advantagesList;

        await repository.UpdateAsync(product, cancellationToken);
    }
}