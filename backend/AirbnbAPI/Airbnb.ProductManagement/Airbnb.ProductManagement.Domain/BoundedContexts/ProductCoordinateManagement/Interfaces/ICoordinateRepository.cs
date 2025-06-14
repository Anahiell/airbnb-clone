using System.Linq.Expressions;
using Airbnb.Domain.BoundedContexts.CoordinatesManagement.Aggregates;
using Airbnb.SharedKernel.Repositories;

namespace Airbnb.Domain.BoundedContexts.ProductCoordinateManagement.Interfaces;

public interface ICoordinateRepository : IRepository<Coordinate>
{
    Task<IEnumerable<Coordinate>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default);
    
    Task DeleteAsync(Expression<Func<Coordinate, bool>> predicate, CancellationToken cancellationToken = default);
}