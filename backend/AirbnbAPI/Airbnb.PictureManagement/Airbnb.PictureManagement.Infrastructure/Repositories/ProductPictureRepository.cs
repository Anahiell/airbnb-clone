using Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Aggregates;
using Airbnb.PictureManagement.Infrastructure.DataContext;
using Airbnb.SharedKernel.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.PictureManagement.Infrastructure.Repositories;

public class ProductPictureRepository(AirbnbPictureDbContext context) : IRepository<ProductPicture>
{
    public async Task<int> AddAsync(ProductPicture picture, CancellationToken cancellationToken = default)
    {
        var entityEntry = await context.ProductPictures.AddAsync(picture, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entityEntry.Entity.Id;
    }

    public async Task<ProductPicture?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.ProductPictures.FindAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<ProductPicture>?> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.ProductPictures.ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(ProductPicture entity, CancellationToken cancellationToken = default)
    {
        context.ProductPictures.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await context.ProductPictures.FindAsync(id, cancellationToken);
        if (entity == null) return;
        context.ProductPictures.Remove(entity);
        await context.SaveChangesAsync(cancellationToken);
    }
}