using Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Aggregates;
using Airbnb.PictureManagement.Infrastructure.DataContext;
using Airbnb.SharedKernel.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.PictureManagement.Infrastructure.Repositories;

public class PictureRepository(AirbnbPictureDbContext context) : IRepository<DomainPicture>
{
    public async Task<int> AddAsync(DomainPicture picture, CancellationToken cancellationToken = default)
    {
        var entityEntry = await context.DomainPicture.AddAsync(picture, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entityEntry.Entity.Id;
    }

    public async Task<DomainPicture?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.DomainPicture.FindAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<DomainPicture>?> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.DomainPicture.ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(DomainPicture entity, CancellationToken cancellationToken = default)
    {
        context.DomainPicture.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await context.DomainPicture.FindAsync(id, cancellationToken);
        if (entity == null) return;
        context.DomainPicture.Remove(entity);
        await context.SaveChangesAsync(cancellationToken);
    }
}