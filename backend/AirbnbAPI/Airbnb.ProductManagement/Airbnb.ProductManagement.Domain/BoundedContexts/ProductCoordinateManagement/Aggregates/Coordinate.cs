using Airbnb.Domain.BoundedContexts.ProductCoordinateManagement.Events;
using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.CoordinatesManagement.Aggregates;

public class Coordinate : AggregateRoot
{
    public string Latitude { get; private set; }
    public string Longitude { get; private set; }
    public int ProductId { get; private set; }
    public Coordinate() { }

    public Coordinate(string latitude, string longitude, int productId)
    {
        Latitude = latitude;
        Longitude = longitude;
        ProductId = productId;
        RaiseEvent(new CoordinateCreatedEvent(Id, latitude, longitude, productId));
    }

    public void Update(string latitude, string longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
        RaiseEvent(new CoordinateUpdatedEvent(Id, latitude, longitude));
    }

    public void Delete()
    {
        RaiseEvent(new CoordinateDeletedEvent(Id));
    }

    #region Event Handling

    protected override void When(IDomainEvent @event)
    {
        switch (@event)
        {
            case CoordinateCreatedEvent e:
                OnCoordinatesCreatedEvent(e);
                break;
            case CoordinateUpdatedEvent e:
                OnCoordinatesUpdatedEvent(e);
                break;
            case CoordinateDeletedEvent e:
                OnCoordinatesDeletedEvent(e);
                break;
        }
    }

    private void OnCoordinatesCreatedEvent(CoordinateCreatedEvent @event)
    {
        Id = @event.AggregateId;
        Latitude = @event.Latitude;
        Longitude = @event.Longitude;
    }

    private void OnCoordinatesUpdatedEvent(CoordinateUpdatedEvent @event)
    {
        Id = @event.AggregateId;
        Latitude = @event.Latitude;
        Longitude = @event.Longitude;
    }

    private void OnCoordinatesDeletedEvent(CoordinateDeletedEvent @event)
    {
        Id = @event.AggregateId;
    }

    #endregion
}