namespace Airbnb.MongoRepository.Interfaces;

public interface IProjectionRepository<T> : IQueryRepository<T>
{
    Task InsertAsync(T entity);
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(int Id);
    Task UpsertAsync(T entity);
}