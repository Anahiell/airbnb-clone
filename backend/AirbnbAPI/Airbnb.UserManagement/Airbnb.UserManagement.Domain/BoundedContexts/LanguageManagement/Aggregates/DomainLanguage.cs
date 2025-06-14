using Airbnb.SharedKernel;
using Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Events;

namespace Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Aggregates;

public class DomainLanguage : AggregateRoot
{
    public string Name { get; private set; }

    public DomainLanguage()
    {
    }

    public DomainLanguage(int id, string name)
    {
        RaiseEvent(new LanguageCreatedEvent(id, name, DateTime.UtcNow));
    }

    #region Aggregate Methods

    public void UpdateName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("Language name cannot be empty.", nameof(newName));

        RaiseEvent(new LanguageUpdatedEvent(Id, newName));
    }

    public void Delete()
    {
        RaiseEvent(new LanguageDeletedEvent(Id));
    }

    #endregion

    #region Event Handling

    protected override void When(IDomainEvent @event)
    {
        switch (@event)
        {
            case LanguageCreatedEvent e:
                OnLanguageCreated(e);
                break;
            case LanguageUpdatedEvent e:
                OnLanguageUpdated(e);
                break;
            case LanguageDeletedEvent e:
                OnLanguageDeleted(e);
                break;
        }
    }

    private void OnLanguageCreated(LanguageCreatedEvent e)
    {
        Id = e.AggregateId;
        Name = e.Name;
    }

    private void OnLanguageUpdated(LanguageUpdatedEvent e)
    {
        Name = e.NewName;
    }

    private void OnLanguageDeleted(LanguageDeletedEvent e)
    {
        Id = e.AggregateId;
    }

    #endregion
}