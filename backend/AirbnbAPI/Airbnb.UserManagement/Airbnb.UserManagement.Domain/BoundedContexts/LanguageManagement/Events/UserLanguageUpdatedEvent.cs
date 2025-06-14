using Airbnb.SharedKernel;

namespace Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Events;

public class UserLanguageUpdatedEvent : DomainEvent
{
    public int UserId { get; }
    public int NewLanguageId { get; }
    
    public string Name { get; }

    public UserLanguageUpdatedEvent(int aggregateId, int userId, int newLanguageId, string name = null)
        : base(aggregateId)
    {
        UserId = userId;
        NewLanguageId = newLanguageId;
        Name = name;
    }
}