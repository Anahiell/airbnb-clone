using Airbnb.MongoRepository.Entities;

namespace Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;

public class ProductEntityInfo : IQueryEntity
{
    public string? Title { get; set; }

    public string? Description { get; set; }
    public int Price { get; set; }

    public int Rating { get; set; }
    public int UserId { get; set; }

    public bool Availability { get; set; }

    public int ApartmentTypeId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int AddressLegalId { get; set; }
    
    public List<ReviewInfo>? Review { get; set; }
    public List<TagInfo>? Tags { get; set; }
    public List<OrderInfo>? Orders { get; set; }
    public List<PictureInfo>? Pictures { get; set; }
    
    public void AddOrders(IEnumerable<OrderInfo> orders) =>
        Orders = orders.Where(o => o.ProductId == Id).ToList();

    public void AddPictures(IEnumerable<PictureInfo> pictures) =>
        Pictures = pictures.Where(p => p.UserId == UserId).ToList();

    public void AddReviews(IEnumerable<ReviewInfo> reviews) =>
        Review = reviews.Where(r => r.ProductId == AddressLegalId).ToList();

    public void AddTags(IEnumerable<TagInfo> tags) =>
        Tags = tags.ToList();
}

public class OrderInfo
{
    public int ProductId { get; set; }
    public int UserId { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
    public int Id { get; set; }
}

public class PictureInfo
{
    public string Url { get; set; }
    public string? Description { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Id { get; set; }
}

public class ReviewInfo
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int Rating { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public int Id { get; set; }
}

public class TagInfo
{
    public string Name { get; set; }
    public int Id { get; set; }
}