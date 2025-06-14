using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductRulesManagement.Events;

public class RuleUpdatedEvent : IDomainEvent
{
    public int AggregateId { get; }
    public int MaxGuestsNumber { get; }
    public bool PetsAllowed { get; }
    public int? MaxPetsNumber { get; }
    public int? PetsAddedPrice { get; }

    public RuleUpdatedEvent(int aggregateId, int maxGuests, bool petsAllowed, int? maxPets, int? petPrice)
    {
        AggregateId = aggregateId;
        MaxGuestsNumber = maxGuests;
        PetsAllowed = petsAllowed;
        MaxPetsNumber = maxPets;
        PetsAddedPrice = petPrice;
    }
}