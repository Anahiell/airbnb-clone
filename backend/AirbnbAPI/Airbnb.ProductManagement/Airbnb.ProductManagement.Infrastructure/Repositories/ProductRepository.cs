using Airbnb.Domain;
using Airbnb.Domain.BoundedContexts.ProductManagement.Interfaces;
using Airbnb.Infrastructure.DataContext;
using Airbnb.Infrastructure.Entities;
using Airbnb.SharedKernel.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace Airbnb.Infrastructure.Repositories;

public class ProductRepository(AirbnbDbContext context) : IRepository<DomainProduct>
{
    public async Task<int> AddAsync(DomainProduct product, CancellationToken cancellationToken = default)
    {
        var entityEntry = await context.DomainProduct.AddAsync(product, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entityEntry.Entity.Id;
    }

    public async Task<DomainProduct?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.DomainProduct.FindAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<DomainProduct>?> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.DomainProduct.ToListAsync(cancellationToken);
    }
    
    public async Task UpdateAsync(DomainProduct entity, CancellationToken cancellationToken = default)
    {
        context.DomainProduct.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await context.DomainProduct.FindAsync(id, cancellationToken);
        if (entity == null) return;
        context.DomainProduct.Remove(entity);
        await context.SaveChangesAsync(cancellationToken);
    }
}