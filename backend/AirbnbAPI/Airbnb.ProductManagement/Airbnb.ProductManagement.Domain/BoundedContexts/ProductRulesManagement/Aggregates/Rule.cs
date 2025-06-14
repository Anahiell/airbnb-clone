using Airbnb.Domain.BoundedContexts.ProductRulesManagement.Events;
using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductRulesManagement.Aggregates;

public class Rule : AggregateRoot
{
    public int MaxGuestsNumber { get; private set; }
    public bool PetsAllowed { get; private set; }
    public int? MaxPetsNumber { get; private set; }
    public int? PetsAddedPrice { get; private set; }
    public int? ProductId { get; private set; }

    public Rule() { }

    public Rule(int productId, int maxGuests, bool petsAllowed, int? maxPets, int? petPrice)
    {
        ProductId = productId;
        MaxGuestsNumber = maxGuests;
        PetsAllowed = petsAllowed;
        MaxPetsNumber = maxPets;
        PetsAddedPrice = petPrice;
        RaiseEvent(new RuleCreatedEvent(Id, productId, maxGuests, petsAllowed, maxPets, petPrice));
    }

    public void Update(int maxGuests, bool petsAllowed, int? maxPets, int? petPrice)
    {
        MaxGuestsNumber = maxGuests;
        PetsAllowed = petsAllowed;
        MaxPetsNumber = maxPets;
        PetsAddedPrice = petPrice;
        RaiseEvent(new RuleUpdatedEvent(Id, maxGuests, petsAllowed, maxPets, petPrice));
    }

    public void Delete()
    {
        RaiseEvent(new RuleDeletedEvent(Id));
    }

    #region Event Handling

    protected override void When(IDomainEvent @event)
    {
        switch (@event)
        {
            case RuleCreatedEvent e:
                OnRuleCreated(e);
                break;
            case RuleUpdatedEvent e:
                OnRuleUpdated(e);
                break;
            case RuleDeletedEvent e:
                OnRuleDeleted(e);
                break;
        }
    }

    private void OnRuleCreated(RuleCreatedEvent @event)
    {
        Id = @event.AggregateId;
        MaxGuestsNumber = @event.MaxGuestsNumber;
        PetsAllowed = @event.PetsAllowed;
        MaxPetsNumber = @event.MaxPetsNumber;
        PetsAddedPrice = @event.PetsAddedPrice;
    }

    private void OnRuleUpdated(RuleUpdatedEvent @event)
    {
        Id = @event.AggregateId;
        MaxGuestsNumber = @event.MaxGuestsNumber;
        PetsAllowed = @event.PetsAllowed;
        MaxPetsNumber = @event.MaxPetsNumber;
        PetsAddedPrice = @event.PetsAddedPrice;
    }

    private void OnRuleDeleted(RuleDeletedEvent @event)
    {
        Id = @event.AggregateId;
    }

    #endregion
}