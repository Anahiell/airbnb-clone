using Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Aggregates;
using Airbnb.PictureManagement.Infrastructure.DataContext;
using Airbnb.SharedKernel.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.PictureManagement.Infrastructure.Repositories;

public class UserPictureRepository(AirbnbPictureDbContext context) : IRepository<UserPicture>
{
    public async Task<int> AddAsync(UserPicture picture, CancellationToken cancellationToken = default)
    {
        var entityEntry = await context.UserPictures.AddAsync(picture, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entityEntry.Entity.Id;
    }

    public async Task<UserPicture?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.UserPictures.FindAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<UserPicture>?> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.UserPictures.ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(UserPicture entity, CancellationToken cancellationToken = default)
    {
        context.UserPictures.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await context.UserPictures.FindAsync(id, cancellationToken);
        if (entity == null) return;
        context.UserPictures.Remove(entity);
        await context.SaveChangesAsync(cancellationToken);
    }
}