using Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Events;
using Airbnb.SharedKernel;

namespace Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Aggregates;

public class UserPicture : AggregateRoot
{
    public string Url { get; private set; }
    public int UserId { get; private set; }
    public Guid PictureGuid { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public bool IsArchived { get; private set; }
    
    public UserPicture() { }

    public UserPicture(Guid pictureGuid, string url, int userId, DateTime createdAt)
    {
        PictureGuid = pictureGuid;
        Url = url;
        UserId = userId;
        CreatedAt = createdAt;
        IsArchived = false;
        
        RaiseEvent(new UserPictureCreatedEvent(Id, url, userId, createdAt));
    }

    public void Archive()
    {
        if (IsArchived)
            return;

        IsArchived = true;
        RaiseEvent(new UserPictureArchivedEvent(Id));
    }

    public void UpdatePictureUrl(string newUrl)
    {
        Url = newUrl;

        RaiseEvent(new UserPictureUpdatedEvent(Id, Url));
    }
    protected override void When(IDomainEvent @event)
    {
        switch (@event)
        {
            case UserPictureCreatedEvent e:
                Id = e.AggregateId;
                Url = e.Url;
                UserId = e.UserId;
                CreatedAt = e.CreatedDate;
                break;
        }
    }
}