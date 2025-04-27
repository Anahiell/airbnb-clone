using Airbnb.SharedKernel;
using Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Events;

namespace Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Aggregates;

public class DomainPicture : AggregateRoot
{
    public string Url { get; private set; }
    public int UserId { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public DomainPicture()
    {
    }

    public DomainPicture(string url, int userId, DateTime createdAt)
    {
        Url = url;
        UserId = userId;
        CreatedAt = createdAt;

        RaiseEvent(new PictureCreatedEvent(Id, url, userId, createdAt));
    }

    #region Aggregate Methods

    public void CreatePicture(string url, int userId, DateTime createdAt)
    {
        Url = url;
        UserId = userId;
        CreatedAt = createdAt;

        RaiseEvent(new PictureCreatedEvent(Id, url, userId, createdAt));
    }

    public void UpdatePicture(string url)
    {
        Url = url;
        RaiseEvent(new PictureUpdatedEvent(Id, url));
    }

    public void DeletePicture()
    {
        RaiseEvent(new PictureDeletedEvent(Id));
    }

    #endregion

    #region Event Handling

    protected override void When(IDomainEvent @event)
    {
        switch (@event)
        {
            case PictureCreatedEvent e:
                OnPictureCreatedEvent(e);
                break;
            case PictureUpdatedEvent e:
                OnPictureUpdatedEvent(e);
                break;
            case PictureDeletedEvent e:
                OnPictureDeletedEvent(e);
                break;
        }
    }

    private void OnPictureCreatedEvent(PictureCreatedEvent @event)
    {
        Id = @event.AggregateId;
        Url = @event.Url;
        UserId = @event.UserId;
        CreatedAt = @event.CreatedDate;
    }

    private void OnPictureUpdatedEvent(PictureUpdatedEvent @event)
    {
        Id = @event.AggregateId;
        Url = @event.Url;
    }

    private void OnPictureDeletedEvent(PictureDeletedEvent @event)
    {
        Id = @event.AggregateId;
    }

    #endregion
}
