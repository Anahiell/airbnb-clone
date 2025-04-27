using Airbnb.SharedKernel;

namespace Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Events;

public class PictureCreatedEvent : DomainEvent
{
    public string Url { get; private set; }
    public int UserId { get; private set; }
    public DateTime CreatedDate { get; private set; }

    public PictureCreatedEvent(int aggregateId, string url, int userId, DateTime createdDate)
        : base(aggregateId)
    {
        Url = url;
        UserId = userId;
        CreatedDate = createdDate;
    }
}