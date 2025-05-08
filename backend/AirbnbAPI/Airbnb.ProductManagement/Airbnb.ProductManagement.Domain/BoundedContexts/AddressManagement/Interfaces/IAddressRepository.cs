using Airbnb.Domain.BoundedContexts.AddressManagement.Aggregates;

namespace Airbnb.Domain.BoundedContexts.AddressManagement.Interfaces;

public interface IAddressRepository
{
    Task<AddressLegal?> GetByIdAsync(int id, CancellationToken cancellationToken);
}