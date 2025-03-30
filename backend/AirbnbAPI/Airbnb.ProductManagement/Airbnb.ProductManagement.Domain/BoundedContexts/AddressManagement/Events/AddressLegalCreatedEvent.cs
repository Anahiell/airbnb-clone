using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.AddressManagement.Events;

public class AddressLegalCreatedEvent : DomainEvent
{
    public int AddressLegalId { get; private set; }
    public string Region { get; private set; }
    public string Country { get; private set; }
    public string City { get; private set; }
    public string District { get; private set; }
    public string House { get; private set; }
    public string? Block { get; private set; }
    public string? Flat { get; private set; }

    public AddressLegalCreatedEvent(int aggregateId, string region, string country, string city, string district, 
        string house, string? block = null, string? flat = null)
        : base(aggregateId)
    {
        AddressLegalId = aggregateId;
        Region = region;
        Country = country;
        City = city;
        District = district;
        House = house;
        Block = block;
        Flat = flat;
    }
}