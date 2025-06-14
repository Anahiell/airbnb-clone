using Airbnb.SharedKernel.Repositories;

namespace Airbnb.Domain.BoundedContexts.ProductFacilityManagement.Facility.Interfaces;

public interface IFacilityRepository : IRepository<Aggregates.Facility>
{
    Task<IEnumerable<Aggregates.Facility>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default);
    Task<Aggregates.Facility?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}