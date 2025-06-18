using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductAdvantageManagement.Events;

public class ProductAdvantagesUpdatedEvent : DomainEvent
{
    public int ProductId { get; }
    public List<(int Id, string Title, string Description)> Advantages { get; }

    public ProductAdvantagesUpdatedEvent(int productId, List<(int Id, string Name, string IconName)> advantages)
    {
        ProductId = productId;
        Advantages = advantages;
    }
}