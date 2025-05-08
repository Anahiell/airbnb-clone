using Airbnb.MongoRepository.Entities;

namespace Airbnb.TagsManagement.Application.BoundedContext.ProductTagManagement.QueryObjects;

public class ProductTagEntityInfo : IQueryEntity
{
    public int ProductId { get; set; }
    public int TagId { get; set; }
    public string TagName { get; set; }
}