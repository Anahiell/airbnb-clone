using Airbnb.MongoRepository.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Airbnb.PictureManagement.Application.BoundedContext.QueryObjects;

public class PictureEntityInfo : IQueryEntity
{
    public string Url { get; set; } = default!;
    public int? ProductId { get; set; }
    public DateTime CreatedAt { get; set; }
}