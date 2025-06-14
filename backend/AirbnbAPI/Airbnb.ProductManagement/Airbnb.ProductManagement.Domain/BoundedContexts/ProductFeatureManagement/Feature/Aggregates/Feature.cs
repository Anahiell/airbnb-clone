using Airbnb.Domain.BoundedContexts.ProductFeatureManagement.Feature.Events;
using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductFeatureManagement.Feature.Aggregates;

public class Feature : AggregateRoot
{
    public string Name { get; private set; }
    public bool Forcibly { get; private set; }
    public int Price { get; private set; }

    public Feature() { }

    public Feature(string name, bool forcibly, int price)
    {
        Name = name;
        Forcibly = forcibly;
        Price = price;
        RaiseEvent(new FeatureCreatedEvent(Id, name, forcibly, price));
    }

    public void Update(string name, bool forcibly, int price)
    {
        Name = name;
        Forcibly = forcibly;
        Price = price;
        RaiseEvent(new FeatureUpdatedEvent(Id, name, forcibly, price));
    }

    public void Delete()
    {
        RaiseEvent(new FeatureDeletedEvent(Id));
    }

    #region Event Handling

    protected override void When(IDomainEvent @event)
    {
        switch (@event)
        {
            case FeatureCreatedEvent e:
                OnFeatureCreatedEvent(e);
                break;
            case FeatureUpdatedEvent e:
                OnFeatureUpdatedEvent(e);
                break;
            case FeatureDeletedEvent e:
                OnFeatureDeletedEvent(e);
                break;
        }
    }

    private void OnFeatureCreatedEvent(FeatureCreatedEvent @event)
    {
        Id = @event.AggregateId;
        Name = @event.Name;
        Forcibly = @event.Forcibly;
        Price = @event.Price;
    }

    private void OnFeatureUpdatedEvent(FeatureUpdatedEvent @event)
    {
        Id = @event.AggregateId;
        Name = @event.Name;
        Forcibly = @event.Forcibly;
        Price = @event.Price;
    }

    private void OnFeatureDeletedEvent(FeatureDeletedEvent @event)
    {
        Id = @event.AggregateId;
    }

    #endregion
}