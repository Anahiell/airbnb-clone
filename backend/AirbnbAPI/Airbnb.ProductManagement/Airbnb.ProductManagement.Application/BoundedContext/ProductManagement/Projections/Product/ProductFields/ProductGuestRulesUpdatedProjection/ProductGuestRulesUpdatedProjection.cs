using Airbnb.Domain.BoundedContexts.ProductRulesManagement.Events;
using Airbnb.MongoRepository.Interfaces;
using Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;
using MediatR;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductManagement.Projections.Product.ProductGuestRulesUpdatedEvent;

public class ProductGuestRulesUpdatedEventHandler(IProjectionRepository<ProductEntityInfo> repository)
    : INotificationHandler<RuleUpdatedEvent>
{
    public async Task Handle(RuleUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var product = await repository.FindByIdAsync(notification.AggregateId, cancellationToken);
        if (product is null)
        {
            return;
        }

        product.GuestRules = new RuleEntityInfo
        {
            Id = notification.AggregateId,
            MaxGuestsNumber = notification.MaxGuestsNumber,
            PetsAllowed = notification.PetsAllowed,
            MaxPetsNumber = notification.MaxPetsNumber,
            PetsAddedPrice = notification.PetsAddedPrice,
        };

        await repository.UpdateAsync(product, cancellationToken);
    }
}