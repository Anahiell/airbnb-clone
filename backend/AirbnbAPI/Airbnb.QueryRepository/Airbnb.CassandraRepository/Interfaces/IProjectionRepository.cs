namespace Airbnb.CassandraRepository.Interfaces;

public interface IProjectionRepository<T> : IQueryRepository<T>
{
    Task InsertAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
}