using Airbnb.Domain.BoundedContexts.ProductAdditionalManagement.Events.Additional;
using Airbnb.MongoRepository.Interfaces;
using Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;
using MediatR;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductManagement.Projections.Product.ProductAdditionalUpdatedEvent;

public class ProductAdditionalInfoUpdatedEventHandler(IProjectionRepository<ProductEntityInfo> repository)
    : INotificationHandler<AdditionalUpdatedEvent>
{
    public async Task Handle(AdditionalUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var product = await repository.FindByIdAsync(notification.AggregateId, cancellationToken);
        if (product is null)
        {
            return;
        }

        product.CancelPolicy = new CancelPolicyEntityInfo
        {
            FreeCancelationDays = notification.CancelPolicy.FreeCancelationDays,
            PartCancelationDays = notification.CancelPolicy.PartCancelationDays,
            PartCancelationPercent = notification.CancelPolicy.PartCancelationPercent,
        };

        var homeRulesTexts = new List<HomeRulesEntityInfo>();
        foreach (var homeRule in notification.HomeRules)
        {
            homeRulesTexts.Add(new HomeRulesEntityInfo
            {
                Type = homeRule.Type,
                Text = homeRule.Text,
            });
        }
        product.HomeRules = homeRulesTexts;

        var safetyRulesLabels = new List<SafetyRulesEntityInfo>();
        foreach (var safetyRule in notification.SafetyRules)
        {
            safetyRulesLabels.Add(new SafetyRulesEntityInfo
            {
                Label = safetyRule.Label,
            });
        }
        product.SafetyRules = safetyRulesLabels;

        await repository.UpdateAsync(product, cancellationToken);
    }
}