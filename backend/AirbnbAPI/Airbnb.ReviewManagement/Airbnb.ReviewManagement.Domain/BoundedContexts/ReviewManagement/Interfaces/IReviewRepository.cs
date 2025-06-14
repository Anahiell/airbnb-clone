using System.Linq.Expressions;
using Airbnb.ReviewManagement.Domain.BoundedContexts.ReviewManagement.Aggregates;
using Airbnb.SharedKernel.Repositories;

namespace Airbnb.ReviewManagement.Domain.BoundedContexts.ReviewManagement.Interfaces;

public interface IReviewRepository : IRepository<DomainReview>
{
    Task DeleteWhereAsync(Expression<Func<DomainReview, bool>> predicate, CancellationToken cancellationToken = default);
}