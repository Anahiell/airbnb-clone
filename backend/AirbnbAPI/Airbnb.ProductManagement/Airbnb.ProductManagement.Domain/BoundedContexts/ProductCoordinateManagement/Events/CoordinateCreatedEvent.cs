using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductCoordinateManagement.Events;

public class CoordinateCreatedEvent : IDomainEvent
{
    public int AggregateId { get; }
    public string Latitude { get; }
    public string Longitude { get; }

    public int ProductId { get; private set; }

    public CoordinateCreatedEvent(int aggregateId, string latitude, string longitude, int productId)
    {
        AggregateId = aggregateId;
        Latitude = latitude;
        Longitude = longitude;
        ProductId = productId;
    }
}