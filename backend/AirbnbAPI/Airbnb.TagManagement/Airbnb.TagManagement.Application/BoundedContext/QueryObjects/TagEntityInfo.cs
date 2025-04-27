using Airbnb.MongoRepository.Entities;

namespace Airbnb.TagsManagement.Application.BoundedContext.QueryObjects;

public class TagEntityInfo : IQueryEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}