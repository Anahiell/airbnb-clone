using System.Linq.Expressions;
using Airbnb.MongoRepository.Entities;
using MongoDB.Driver;

namespace Airbnb.MongoRepository.Repositories;

public abstract class BaseMongoRepository<T> where T : IQueryEntity
{
    protected readonly IMongoDatabase _mongoDatabase;
    protected abstract string CollectionName { get; }

    protected BaseMongoRepository(IMongoDatabase mongoDatabase)
    {
        _mongoDatabase = mongoDatabase;
    }

    /// <summary>
    /// Применяет фильтрацию.
    /// </summary>
    protected virtual IQueryable<T> ApplyFilters(IQueryable<T> query, Expression<Func<T, bool>>? filter)
    {
        return filter != null ? query.Where(filter) : query;
    }

    /// <summary>
    /// Применяет сортировку.
    /// </summary>
    protected virtual IFindFluent<T, T> ApplySorting(IFindFluent<T, T> query, SortDefinition<T>? sort)
    {
        return sort != null ? query.Sort(sort) : query;
    }

    /// <summary>
    /// Применяет пагинацию.
    /// </summary>
    protected virtual IFindFluent<T, T> ApplyPagination(IFindFluent<T, T> query, int page, int pageSize)
    {
        return query.Skip((page - 1) * pageSize).Limit(pageSize);
    }

    /// <summary>
    /// Выполняет запрос с фильтрацией, сортировкой и пагинацией.
    /// </summary>
    public async Task<(IEnumerable<T> Items, long TotalCount)> GetFilteredPaginatedAsync(
        Expression<Func<T, bool>>? filter = null,
        SortDefinition<T>? sort = null,
        int page = 1,
        int pageSize = 10)
    {
        var collection = _mongoDatabase.GetCollection<T>(CollectionName);

        var totalCount = await collection.CountDocumentsAsync(filter ?? (_ => true)); // Считаем количество записей

        var query = collection.Find(filter ?? (_ => true)); // Применяем фильтр
        query = ApplySorting(query, sort); // Применяем сортировку
        query = ApplyPagination(query, page, pageSize); // Применяем пагинацию

        var result = await query.ToListAsync();
        return (result, totalCount);
    }
}