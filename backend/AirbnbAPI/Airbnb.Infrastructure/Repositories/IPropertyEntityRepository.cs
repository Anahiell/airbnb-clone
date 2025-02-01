using Airbnb.Infrastructure.Entities;

namespace Airbnb.Infrastructure.Repositories;

public interface IPropertyEntityRepository
{
    Task CreatePropertyEntity(PropertyEntity propertyEntity);
    Task<PropertyEntity> GetPropertyEntityById(Guid propertyEntity);
    Task<IEnumerable<PropertyEntity>> GetAllPropertyEntity();
}