namespace Airbnb.MongoRepository.Interfaces;

public interface IProjectionRepository<T> : IQueryRepository<T>
{
    Task InsertAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int Id);
    Task UpsertAsync(T entity);
}