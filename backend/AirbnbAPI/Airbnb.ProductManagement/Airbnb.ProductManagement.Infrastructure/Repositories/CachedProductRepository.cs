using Airbnb.Cache;
using Airbnb.Cache.Repository;
using Airbnb.Domain;
using Airbnb.SharedKernel.Repositories;

namespace Airbnb.Infrastructure.Repositories;

public class CachedProductRepository(ICacheService cache,
    IRepository<DomainProduct> repository)
    : CachedRepositoryBase<DomainProduct>(cache, repository)
{
    protected override int GetId(DomainProduct entity) => entity.Id;

    protected override TimeSpan CacheDuration => TimeSpan.FromMinutes(15);
}