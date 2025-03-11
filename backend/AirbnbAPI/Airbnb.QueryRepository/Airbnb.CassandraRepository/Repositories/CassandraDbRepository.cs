using System.Linq.Expressions;
using Airbnb.CassandraRepository.Entities;
using Airbnb.CassandraRepository.Exceptions;
using Airbnb.CassandraRepository.Interfaces;
using Cassandra;

namespace Airbnb.CassandraRepository.Repositories;

public class CassandraDbRepository<T>(ISession session) : IProjectionRepository<T>
    where T : IQueryEntity
{
    private readonly ISession _session = session ?? throw new ArgumentNullException(nameof(session));
    private static string TableName => typeof(T).Name.ToLower();

    #region Queries

    public async Task<IEnumerable<T>> FindAllAsync()
    {
        var query = new SimpleStatement($"SELECT * FROM {TableName}");
        var result = await _session.ExecuteAsync(query);
        return result.Select(row => MapRowToEntity(row)).ToList();
    }

    public async Task<T> FindByIdAsync(int id)
    {
        var prepared = await _session.PrepareAsync($"SELECT * FROM {TableName} WHERE id = ?");
        var bound = prepared.Bind(id);
        var result = await _session.ExecuteAsync(bound);
        return result.FirstOrDefault() != null ? MapRowToEntity(result.First()) : null;
    }

    #endregion

    #region Projections

    public async Task InsertAsync(T entity)
    {
        try
        {
            var prepared = await _session.PrepareAsync($"INSERT INTO {TableName} (id, version) VALUES (?, ?)");
            var bound = prepared.Bind(entity.Id);
            await _session.ExecuteAsync(bound);
        }
        catch (Exception ex)
        {
            throw new CassandraDbException($"Error inserting entity with ID {entity.Id}.", ex);
        }
    }

    public async Task UpdateAsync(T entity)
    {
        try
        {
            var prepared = await _session.PrepareAsync($"UPDATE {TableName} SET version = ? WHERE id = ?");
            var bound = prepared.Bind(entity.Id);
            await _session.ExecuteAsync(bound);
        }
        catch (Exception ex)
        {
            throw new CassandraDbException($"Error updating entity with ID {entity.Id}.", ex);
        }
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            var prepared = await _session.PrepareAsync($"DELETE FROM {TableName} WHERE id = ?");
            var bound = prepared.Bind(id);
            await _session.ExecuteAsync(bound);
        }
        catch (Exception ex)
        {
            throw new CassandraDbException($"Error deleting entity with ID {id}.", ex);
        }
    }

    #endregion

    private T MapRowToEntity(Row row)
    {
        // TODO маппер в доменные модели
        var entity = Activator.CreateInstance<T>();
        entity.Id = row.GetValue<int>("id");
        return entity;
    }
}