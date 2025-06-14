using Airbnb.SharedKernel.Repositories;

namespace Airbnb.Domain.BoundedContexts.ProductFeatureManagement.Feature.Interfaces;

public interface IFeatureRepository : IRepository<Aggregates.Feature>
{
    Task<IEnumerable<Aggregates.Feature>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default);
    
    Task<Aggregates.Feature?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}