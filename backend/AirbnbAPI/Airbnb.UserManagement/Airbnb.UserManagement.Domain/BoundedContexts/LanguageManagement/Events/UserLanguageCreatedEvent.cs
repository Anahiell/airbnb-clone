using Airbnb.SharedKernel;

namespace Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Events;

public class UserLanguageCreatedEvent : DomainEvent
{
    public int UserId { get; }
    public int LanguageId { get; }
    public DateTime CreatedAt { get; }
    public string Name { get; }
    public UserLanguageCreatedEvent(int aggregateId, int userId, int languageId, DateTime createdAt, string name = null)
        : base(aggregateId)
    {
        UserId = userId;
        LanguageId = languageId;
        CreatedAt = createdAt;
        Name = name;
    }
}