using System.Linq.Expressions;
using Airbnb.Domain.BoundedContexts.ProductAdditionalInfoManagement.Aggregates;
using Airbnb.SharedKernel.Repositories;

namespace Airbnb.Domain.BoundedContexts.ProductAdditionalManagement.Interfaces;

public interface IAdditionalRepository : IRepository<Additional>
{
    Task DeleteAsync(Expression<Func<Additional, bool>> predicate, CancellationToken cancellationToken = default);
}