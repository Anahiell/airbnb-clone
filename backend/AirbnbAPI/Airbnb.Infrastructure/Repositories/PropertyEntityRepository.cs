using Airbnb.Infrastructure.DataContext;
using Airbnb.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.Infrastructure.Repositories;

public class PropertyEntityRepository(AirbnbDbContext context) : IPropertyEntityRepository
{
    public async Task CreatePropertyEntity(PropertyEntity propertyEntity)
    {
        await context.PropertyEntity.AddAsync(propertyEntity);
        await context.SaveChangesAsync();
    }
    
    public async Task<PropertyEntity> GetPropertyEntityById(Guid propertyEntity)
    {
        return await context.PropertyEntity.FindAsync(propertyEntity) ?? throw new NullReferenceException();
    }
    
    public async Task<IEnumerable<PropertyEntity>> GetAllPropertyEntity()
    {
        return await context.PropertyEntity.ToListAsync();
    }
}