using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.AddressManagement.Events;

public class AddressLegalUpdatedEvent(int aggregateId, string region, string country, string city, string district,
    string house, string? block = null, string? flat = null)
    : DomainEvent(aggregateId)
{
    public int AddressLegalId { get; private set; } = aggregateId;
    public string Region { get; private set; } = region;
    public string Country { get; private set; } = country;
    public string City { get; private set; } = city;
    public string District { get; private set; } = district;
    public string House { get; private set; } = house;
    public string? Block { get; private set; } = block;
    public string? Flat { get; private set; } = flat;
}