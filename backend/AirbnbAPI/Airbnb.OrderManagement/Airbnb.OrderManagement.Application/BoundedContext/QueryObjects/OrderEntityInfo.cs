using Airbnb.MongoRepository.Entities;

namespace Airbnb.OrderManagement.Application.BoundedContext.QueryObjects;

public class OrderEntityInfo : IQueryEntity
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int UserId { get; set; }

    public DateTime DateStart { get; set; }

    public DateTime DateEnd { get; set; }
}