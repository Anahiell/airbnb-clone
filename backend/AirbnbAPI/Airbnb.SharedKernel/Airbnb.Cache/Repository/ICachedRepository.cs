namespace Airbnb.Cache.Repository;

public interface ICachedRepository<TEntity>
{
    Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task SaveAsync(TEntity entity, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
}