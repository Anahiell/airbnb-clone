using Airbnb.SharedKernel.Repositories;

namespace Airbnb.Domain.BoundedContexts.ProductFeatureManagement.ProductFeature.Interfaces;

public interface IProductFeatureRepository
{
    Task<Aggregates.ProductFeature?> GetByProductIdAndFeatureIdAsync(int productId, int featureId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Aggregates.ProductFeature>> GetByProductIdAsync(int productId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Aggregates.ProductFeature>> GetByFeatureIdAsync(int featureId, CancellationToken cancellationToken = default);
    Task<int> AddAsync(Aggregates.ProductFeature productFeature, CancellationToken cancellationToken = default);
    Task UpdateAsync(Aggregates.ProductFeature productFeature, CancellationToken cancellationToken = default);
    Task RemoveAsync(int productId, int featureId, CancellationToken cancellationToken = default);
    Task DeleteAllByProductIdAsync(int productId, CancellationToken cancellationToken = default);
}