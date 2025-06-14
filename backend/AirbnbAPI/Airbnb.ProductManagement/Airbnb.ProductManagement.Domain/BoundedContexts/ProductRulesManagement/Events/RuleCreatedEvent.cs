using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductRulesManagement.Events;

public class RuleCreatedEvent : IDomainEvent
{
    public int AggregateId { get; }
    public int MaxGuestsNumber { get; }
    public bool PetsAllowed { get; }
    public int? MaxPetsNumber { get; }
    public int? PetsAddedPrice { get; }
    public int? ProductId { get; }
    
    public RuleCreatedEvent(int aggregateId, int productId, int maxGuests, bool petsAllowed, int? maxPets, int? petPrice)
    {
        AggregateId = aggregateId;
        ProductId = productId;
        MaxGuestsNumber = maxGuests;
        PetsAllowed = petsAllowed;
        MaxPetsNumber = maxPets;
        PetsAddedPrice = petPrice;
    }
}