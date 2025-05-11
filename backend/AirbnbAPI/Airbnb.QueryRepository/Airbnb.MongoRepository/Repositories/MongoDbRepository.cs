using System.Linq.Expressions;
using Airbnb.MongoRepository.Entities;
using Airbnb.MongoRepository.Exceptions;
using Airbnb.MongoRepository.Interfaces;
using MongoDB.Driver;

namespace Airbnb.MongoRepository.Repositories;

public class MongoDbRepository<T> : BaseMongoRepository<T>, IProjectionRepository<T> where T : IQueryEntity
{
    private readonly IMongoDatabase _mongoDatabase;
    protected override string CollectionName => typeof(T).Name;

    public MongoDbRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
    {
        _mongoDatabase = mongoDatabase;
    }

    #region Queries

    public async Task<IEnumerable<T>> FindAllAsync()
    {
        var cursor = await _mongoDatabase.GetCollection<T>(CollectionName)
            .FindAsync(_ => true);
        return cursor.ToEnumerable();
    }


    public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate)
    {
        var cursor = await _mongoDatabase.GetCollection<T>(CollectionName)
            .FindAsync(predicate);
        return cursor.ToEnumerable();
    }

    public async Task<T> FindByIdAsync(int id)
    {
        return await _mongoDatabase.GetCollection<T>(CollectionName)
            .Find(x => x.Id == id)
            .SingleOrDefaultAsync();
    }

    #endregion

    #region Projections

    public async Task InsertAsync(T entity)
    {
        try
        {
            await _mongoDatabase.GetCollection<T>(CollectionName).InsertOneAsync(entity);
        }
        catch (MongoWriteException ex)
        {
            throw new MongoDbException($"Cannot execute projection for insert of entity {entity.Id}.", ex);
        }
    }

    public async Task UpdateAsync(T entity)
    {
        try
        {
            var result = await _mongoDatabase.GetCollection<T>(CollectionName)
                .ReplaceOneAsync(x => x.Id == entity.Id, entity);

            if (result.MatchedCount != 1)
            {
                throw new MongoDbException($"Cannot find entity {entity.Id} to execute projection for update on.");
            }
        }
        catch (MongoWriteException ex)
        {
            throw new MongoDbException($"Cannot execute projection for update of entity {entity.Id}.", ex);
        }
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            var result = await _mongoDatabase.GetCollection<T>(CollectionName)
                .DeleteOneAsync(x => x.Id == id);
        }
        catch (MongoWriteException ex)
        {
            throw new MongoDbException($"Cannot execute projection for delete of entity with id {id}.", ex);
        }
    }
    
    public async Task UpsertAsync(T entity)
    {
        var filter = Builders<T>.Filter.Eq(e => e.Id, entity.Id);
        var options = new ReplaceOptions { IsUpsert = true };
        await _mongoDatabase.GetCollection<T>(CollectionName)
            .ReplaceOneAsync(filter, entity, options);
    }

    #endregion
}