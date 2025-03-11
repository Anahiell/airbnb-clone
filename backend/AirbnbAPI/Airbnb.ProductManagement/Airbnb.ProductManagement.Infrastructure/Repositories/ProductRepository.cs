using Airbnb.Domain;
using Airbnb.Domain.BoundedContexts.ProductManagement.Interfaces;
using Airbnb.Infrastructure.DataContext;
using Airbnb.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.Infrastructure.Repositories;

public class ProductRepository(AirbnbDbContext context) : IProductRepository
{
    public async Task<int> CreateProductAsync(DomainProduct propertyEntity, CancellationToken cancellationToken = default)
    {
        var entityEntry = await context.DomainProduct.AddAsync(propertyEntity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entityEntry.Entity.Id;
    }
    
    public async Task<DomainProduct> GetProductByIdAsync(int domainProduct)
    {
        return await context.DomainProduct.FindAsync(domainProduct) ?? throw new NullReferenceException();
    }
    
    public async Task<IEnumerable<DomainProduct>> GetAllProductsAsync()
    {
        return await context.DomainProduct.ToListAsync();
    }
}