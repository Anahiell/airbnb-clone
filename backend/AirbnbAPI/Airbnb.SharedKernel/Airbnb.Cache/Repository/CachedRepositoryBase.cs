using Airbnb.SharedKernel.Repositories;

namespace Airbnb.Cache.Repository;

public abstract class CachedRepositoryBase<TEntity>(
    ICacheService cache,
    IRepository<TEntity> repository)
    : IRepository<TEntity> where TEntity : class
{
    protected abstract int GetId(TEntity entity);
    protected virtual TimeSpan CacheDuration => TimeSpan.FromMinutes(10);

    public async Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var cacheKey = $"{typeof(TEntity).Name.ToLower()}-{id}";

        return await cache.GetOrCreateAsync(
            cacheKey,
            async _ => await repository.GetByIdAsync(id, cancellationToken),
            CacheDuration,
            cancellationToken);
    }

    public async Task<IEnumerable<TEntity>?> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var cacheKey = $"{typeof(TEntity).Name.ToLower()}-all";

        return await cache.GetOrCreateAsync(
            cacheKey,
            async _ => await repository.GetAllAsync(cancellationToken),
            CacheDuration,
            cancellationToken);
    }

    public async Task<int> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var id = await repository.AddAsync(entity, cancellationToken);
        var cacheKey = $"{typeof(TEntity).Name.ToLower()}-{id}";

        await cache.GetOrCreateAsync(
            cacheKey,
            _ => Task.FromResult(entity),
            CacheDuration,
            cancellationToken
        );

        return id;
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await repository.UpdateAsync(entity, cancellationToken);
        var cacheKey = $"{typeof(TEntity).Name.ToLower()}-{GetId(entity)}";

        cache.Remove(cacheKey);
        await cache.GetOrCreateAsync(cacheKey, async _ => entity, CacheDuration, cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        await repository.DeleteAsync(id, cancellationToken);
        var cacheKey = $"{typeof(TEntity).Name.ToLower()}-{id}";

        cache.Remove(cacheKey);
    }
}
