using Airbnb.SharedKernel;
using Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Events;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Aggregates;

namespace Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Aggregates;

public class DomainUserLanguage : AggregateRoot
{
    public int UserId { get; private set; }
    public int LanguageId { get; private set; }

    public void UpdateLanguage(int newLanguageId)
    {
        if (newLanguageId == LanguageId)
            return;

        RaiseEvent(new UserLanguageUpdatedEvent(Id, UserId, newLanguageId));
    }
    public DomainUser User { get; private set; }
    public DomainLanguage Language { get; private set; }

    public DomainUserLanguage()
    {
    }

    public DomainUserLanguage(int userId, int languageId)
    {
        RaiseEvent(new UserLanguageCreatedEvent(Id, userId, languageId, DateTime.UtcNow));
    }

    #region Aggregate Methods

    public void Delete()
    {
        RaiseEvent(new UserLanguageRemovedEvent(Id, UserId));
    }

    #endregion

    #region Event Handling

    protected override void When(IDomainEvent @event)
    {
        switch (@event)
        {
            case UserLanguageCreatedEvent e:
                OnUserLanguageAdded(e);
                break;
            case UserLanguageRemovedEvent e:
                OnUserLanguageRemoved(e);
                break;
            case UserLanguageUpdatedEvent e:
                OnUserLanguageUpdated(e);
                break;
        }
    }

    private void OnUserLanguageUpdated(UserLanguageUpdatedEvent e)
    {
        Id = e.AggregateId;
        LanguageId = e.NewLanguageId;
    }

    private void OnUserLanguageAdded(UserLanguageCreatedEvent e)
    {
        Id = e.AggregateId;
        UserId = e.UserId;
        LanguageId = e.LanguageId;
    }

    private void OnUserLanguageRemoved(UserLanguageRemovedEvent e)
    {
        Id = e.AggregateId;
    }

    #endregion
}