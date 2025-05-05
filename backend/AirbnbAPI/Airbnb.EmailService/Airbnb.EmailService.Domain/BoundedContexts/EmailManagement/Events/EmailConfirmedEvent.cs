using Airbnb.SharedKernel;

namespace Airbnb.EmailService.Domain.BoundedContexts.EmailManagement.Events;

public class EmailConfirmedEvent : DomainEvent
{
    public int AggregateId { get; }
    public int UserId { get; }

    public EmailConfirmedEvent(int aggregateId, int userId)
    {
        AggregateId = aggregateId;
        UserId = userId;
    }
}