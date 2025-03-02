using System.Linq.Expressions;

namespace Airbnb.CassandraRepository.Interfaces;

public interface IQueryRepository<T>
{
    Task<IEnumerable<T>> FindAllAsync();
    Task<T> FindByIdAsync(int id);
}