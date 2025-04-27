using Airbnb.ReviewManagement.Domain.BoundedContexts.ReviewManagement.Aggregates;
using Airbnb.ReviewManagementInfrastructure.DataContext;
using Airbnb.SharedKernel.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.ReviewManagementInfrastructure.Repositories;

public class ReviewRepository(AirbnbDbContext context) : IRepository<DomainReview>
{
    public async Task<int> AddAsync(DomainReview review, CancellationToken cancellationToken = default)
    {
        var entityEntry = await context.DomainReview.AddAsync(review, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entityEntry.Entity.Id;
    }

    public async Task<DomainReview?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.DomainReview.FindAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<DomainReview>?> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.DomainReview.ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(DomainReview entity, CancellationToken cancellationToken = default)
    {
        context.DomainReview.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await context.DomainReview.FindAsync(id, cancellationToken);
        if (entity == null) return;
        context.DomainReview.Remove(entity);
        await context.SaveChangesAsync(cancellationToken);
    }
}
