using Airbnb.SharedKernel.Repositories;
using Airbnb.TagsManagement.Domain.BoundedContexts.TagsManagement.Aggregates;
using Airbnb.TagsManagement.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.TagsManagement.Infrastructure.Repositories;

public class TagRepository(AirbnbDbContext context) : IRepository<DomainTag>
{
    public async Task<int> AddAsync(DomainTag tag, CancellationToken cancellationToken = default)
    {
        var entityEntry = await context.DomainTag.AddAsync(tag, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entityEntry.Entity.Id;
    }

    public async Task<DomainTag?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.DomainTag.FindAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<DomainTag>?> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.DomainTag.ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(DomainTag entity, CancellationToken cancellationToken = default)
    {
        context.DomainTag.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await context.DomainTag.FindAsync(id, cancellationToken);
        if (entity == null) return;
        context.DomainTag.Remove(entity);
        await context.SaveChangesAsync(cancellationToken);
    }
}
