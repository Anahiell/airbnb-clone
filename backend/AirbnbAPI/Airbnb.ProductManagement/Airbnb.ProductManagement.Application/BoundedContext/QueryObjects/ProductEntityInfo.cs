using Airbnb.Domain.BoundedContexts.PropertyTypeManagement.ValueObjects;
using Airbnb.MongoRepository.Entities;

namespace Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;

public class ProductEntityInfo : IQueryEntity
{
    public string? Title { get; set; }

    public string? Description { get; set; }
    public int Price { get; set; }

    public double Rating { get; set; }
    public int UserId { get; set; }

    public bool Availability { get; set; }

    public int ApartmentTypeId { get; set; }

    public PropertyTypeEnum? ApartmentType { get; set; }
    public DateTime CreatedDate { get; set; }

    public int AddressLegalId { get; set; }
    
    public string? AddressFull { get; set; }
    public List<ReviewInfo>? Review { get; set; }
    public List<TagInfo>? Tags { get; set; }
    public List<OrderInfo>? Orders { get; set; }
    public List<PictureInfo>? Pictures { get; set; }
    
    public void UpdateOrder(OrderInfo newOrder)
    {
        Orders ??= new List<OrderInfo>();
        var existing = Orders.FirstOrDefault(x => x.Id == newOrder.Id);

        if (existing != null)
        {
            existing.DateStart = newOrder.DateStart;
            existing.DateEnd = newOrder.DateEnd;
            existing.UserId = newOrder.UserId;
            existing.ProductId = newOrder.ProductId;
        }
        else
        {
            Orders.Add(newOrder);
        }
    }

    public void RemoveOrder(int orderId)
    {
        Orders?.RemoveAll(o => o.Id == orderId);
    }
    
    public void RemoveReviews()
    {
        Review = null;
    }

    public void RemoveTags()
    {
        Tags = null;
    }

    public void RemovePictures()
    {
        Pictures = null;
    }
    
    public void RemoveOrder()
    {
        Orders = null;
    }
    public void UpdateReview(ReviewInfo updated)
    {
        Review ??= new List<ReviewInfo>();
    
        var existing = Review.FirstOrDefault(x => x.Id == updated.Id);
        if (existing != null)
        {
            existing.Title = updated.Title;
            existing.Description = updated.Description;
            existing.Rating = updated.Rating;
            existing.UpdatedAt = updated.UpdatedAt;
        }
        else
        {
            Review.Add(updated);
        }
    }

    public void UpdatePicture(PictureInfo updatedPicture)
    {
        Pictures ??= new List<PictureInfo>();
        
        var existing = Pictures?.FirstOrDefault(p => p.Id == updatedPicture.Id);
        if (existing != null)
        {
            existing.Url = updatedPicture.Url;
        }
        else
        {
            Pictures?.Add(updatedPicture);
        }
    }
    
    public void UpdateTag(TagInfo updatedTag)
    {
        Tags ??= new List<TagInfo>();
        
        var existing = Tags?.FirstOrDefault(t => t.Id == updatedTag.Id);
        if (existing != null)
        {
            existing.TagName = updatedTag.TagName;
        }
        else
        {
            Tags?.Add(updatedTag);
        }
    }
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
    public int ProductId { get; set; }
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
    public string TagName { get; set; }
    public int Id { get; set; }
}