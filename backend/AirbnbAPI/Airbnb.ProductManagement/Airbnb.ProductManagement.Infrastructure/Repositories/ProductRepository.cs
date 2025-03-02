using Airbnb.Domain;
using Airbnb.Infrastructure.DataContext;
using Airbnb.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.Infrastructure.Repositories;

public class ProductRepository(AirbnbDbContext context) 
{
    public async Task CreateProductAsync(DomainProduct propertyEntity)
    {
        await context.DomainProduct.AddAsync(propertyEntity);
        await context.SaveChangesAsync();
    }
    
    public async Task<DomainProduct> GetProductyByIdAsync(int domainProduct)
    {
        return await context.DomainProduct.FindAsync(domainProduct) ?? throw new NullReferenceException();
    }
    
    public async Task<IEnumerable<DomainProduct>> GetAllProductsAsync()
    {
        return await context.DomainProduct.ToListAsync();
    }
}