namespace Airbnb.CassandraRepository.Entities;

public abstract class QueryEntity : IQueryEntity
{
    public long Version { get; set; }
}