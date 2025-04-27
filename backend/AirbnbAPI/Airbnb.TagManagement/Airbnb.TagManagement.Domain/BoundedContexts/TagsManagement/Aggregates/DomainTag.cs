using Airbnb.SharedKernel;
using Airbnb.TagsManagement.Domain.BoundedContexts.TagsManagement.Events;

namespace Airbnb.TagsManagement.Domain.BoundedContexts.TagsManagement.Aggregates;

public class DomainTag : AggregateRoot
{
    public string Name { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public DomainTag()
    {
    }

    public DomainTag(string name)
    {
        Name = name;
        CreatedAt = DateTime.UtcNow;

        RaiseEvent(new TagCreatedEvent(Id, name, CreatedAt));
    }

    #region Aggregate Methods

    public void UpdateName(string newName)
    {
        Name = newName;
        RaiseEvent(new TagUpdatedEvent(Id, newName));
    }

    public void Delete()
    {
        RaiseEvent(new TagDeletedEvent(Id));
    }

    #endregion

    #region Event Handling

    protected override void When(IDomainEvent @event)
    {
        switch (@event)
        {
            case TagCreatedEvent e:
                OnTagCreated(e);
                break;
            case TagUpdatedEvent e:
                OnTagUpdated(e);
                break;
            case TagDeletedEvent e:
                OnTagDeleted(e);
                break;
        }
    }

    private void OnTagCreated(TagCreatedEvent e)
    {
        Id = e.AggregateId;
        Name = e.Name;
        CreatedAt = e.CreatedAt;
    }

    private void OnTagUpdated(TagUpdatedEvent e)
    {
        Name = e.NewName;
    }

    private void OnTagDeleted(TagDeletedEvent e)
    {
        Id = e.AggregateId;
    }

    #endregion
}