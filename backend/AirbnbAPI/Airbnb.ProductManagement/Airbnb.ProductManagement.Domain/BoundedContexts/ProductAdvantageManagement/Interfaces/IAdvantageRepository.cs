using System.Linq.Expressions;
using Airbnb.Domain.BoundedContexts.ProductAdvantageManagement.Aggregates;
using Airbnb.SharedKernel.Repositories;

namespace Airbnb.Domain.BoundedContexts.ProductAdvantageManagement.Interfaces;

public interface IAdvantageRepository : IRepository<Advantage>
{
    Task<IEnumerable<Advantage>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default);
    
    Task DeleteAsync(Expression<Func<Advantage, bool>> predicate, CancellationToken cancellationToken = default);

}