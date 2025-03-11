using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Airbnb.MongoRepository.Entities;

public abstract class QueryEntity : IQueryEntity
{
    public long Version { get; set; }
}