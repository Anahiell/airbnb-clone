using Airbnb.SharedKernel;

namespace Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Events;

public class UserLanguagesUpdatedEvent : DomainEvent
{
    public int UserId { get; }
    public List<string?>? LanguageNames { get; }

    public UserLanguagesUpdatedEvent(int userId, List<string?>? languageNames)
    {
        UserId = userId;
        LanguageNames = languageNames;
    }
}