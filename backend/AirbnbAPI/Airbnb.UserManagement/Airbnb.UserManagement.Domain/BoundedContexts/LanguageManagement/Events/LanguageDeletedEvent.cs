using Airbnb.SharedKernel;

namespace Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Events;

public class LanguageDeletedEvent : DomainEvent
{
    public LanguageDeletedEvent(int aggregateId)
        : base(aggregateId)
    {
    }
}