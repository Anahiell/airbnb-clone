using Airbnb.SharedKernel.Repositories;

namespace Airbnb.Domain.BoundedContexts.ProductFacilityManagement.ProductFacility.Interfaces;

public interface IProductFacilityRepository
{
    Task<Aggregates.ProductFacility?> GetByProductIdAndFacilityIdAsync(int productId, int facilityId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Aggregates.ProductFacility>> GetByProductIdAsync(int productId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Aggregates.ProductFacility>> GetByFacilityIdAsync(int facilityId, CancellationToken cancellationToken = default);
    Task<int> AddAsync(Aggregates.ProductFacility productFacility, CancellationToken cancellationToken = default);
    Task UpdateAsync(Aggregates.ProductFacility productFacility, CancellationToken cancellationToken = default);
    Task RemoveAsync(int productId, int facilityId, CancellationToken cancellationToken = default);
    Task DeleteAllByProductIdAsync(int productId, CancellationToken cancellationToken = default);
}