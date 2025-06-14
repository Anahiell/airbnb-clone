using System.Linq.Expressions;
using Airbnb.Domain.BoundedContexts.CoordinatesManagement.Aggregates;
using Airbnb.Domain.BoundedContexts.ProductRulesManagement.Aggregates;
using Airbnb.SharedKernel.Repositories;

namespace Airbnb.Domain.BoundedContexts.ProductRulesManagement.Interfaces;

public interface IRuleRepository : IRepository<Rule>
{
    Task DeleteAsync(Expression<Func<Rule, bool>> predicate, CancellationToken cancellationToken = default);
}