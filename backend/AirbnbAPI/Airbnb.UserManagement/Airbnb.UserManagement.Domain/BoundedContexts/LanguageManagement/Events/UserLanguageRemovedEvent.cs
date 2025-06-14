using Airbnb.SharedKernel;

namespace Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Events;

public class UserLanguageRemovedEvent : DomainEvent
{
    public int UserId { get; set; }
    public UserLanguageRemovedEvent(int aggregateId, int userId)
        : base(aggregateId)
    {
        AggregateId = aggregateId;
        UserId = userId;
    }
}