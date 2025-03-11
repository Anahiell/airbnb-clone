using System.Linq.Expressions;

namespace Airbnb.MongoRepository.Interfaces;

public interface IQueryRepository<T>
{
    Task<IEnumerable<T>> FindAllAsync();
    Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate);
    Task<T> FindByIdAsync(int id);
}