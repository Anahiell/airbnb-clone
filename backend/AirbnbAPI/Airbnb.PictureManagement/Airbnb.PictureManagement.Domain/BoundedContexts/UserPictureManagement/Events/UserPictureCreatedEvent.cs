using Airbnb.SharedKernel;

namespace Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Events;

public class UserPictureCreatedEvent : DomainEvent
{
    public int AggregateId { get; }
    public string Url { get; }
    public int UserId { get; }
    public DateTime CreatedDate { get; }

    public UserPictureCreatedEvent(int id, string url, int userId, DateTime createdDate)
    {
        AggregateId = id;
        Url = url;
        UserId = userId;
        CreatedDate = createdDate;
    }
}