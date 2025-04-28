using Airbnb.MongoRepository.Entities;

namespace Airbnb.TagsManagement.Application.BoundedContext.QueryObjects;

public class TagEntityInfo : IQueryEntity
{
    public string Name { get; set; } = string.Empty;
}