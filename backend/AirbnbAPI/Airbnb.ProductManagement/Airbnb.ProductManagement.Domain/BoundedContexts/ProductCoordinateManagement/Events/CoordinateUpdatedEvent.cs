using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductCoordinateManagement.Events;

public class CoordinateUpdatedEvent : DomainEvent
{
    public int AggregateId { get; }
    public string Latitude { get; }
    public string Longitude { get; }

    public CoordinateUpdatedEvent(int aggregateId, string latitude, string longitude)
    {
        AggregateId = aggregateId;
        Latitude = latitude;
        Longitude = longitude;
    }
}