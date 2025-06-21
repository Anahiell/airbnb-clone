using Airbnb.Domain.BoundedContexts.PropertyTypeManagement.ValueObjects;
using Airbnb.MongoRepository.Entities;
using Airbnb.ProductManagement.Application.BoundedContext.ProductFacilityManagement.QueryObjects;

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
    public RuleEntityInfo GuestRules { get; set; }
    public CancelPolicyEntityInfo CancelPolicy { get; set; }
    public List<HomeRulesEntityInfo> HomeRules { get; set; }
    public List<SafetyRulesEntityInfo> SafetyRules { get; set; }
    public List<AdvantagesEntityInfo> Advantages { get; set; }
    public List<FeatureEntityInfo> Features { get; set; }
    public List<FacilityEntityInfo> Facilities { get; set; }
    public CoordinateEntityInfo Coordinates { get; set; }
    public OwnerEntityInfo Owner { get; set; }
    
    public ProductEntityInfo()
    {
        Coordinates = new CoordinateEntityInfo();
        GuestRules = new RuleEntityInfo();
        CancelPolicy = new CancelPolicyEntityInfo();
        HomeRules = new List<HomeRulesEntityInfo>();
        SafetyRules = new List<SafetyRulesEntityInfo>();
        Advantages = new List<AdvantagesEntityInfo>();
        Features = new List<FeatureEntityInfo>();
        Facilities = new List<FacilityEntityInfo>();
        Owner = new OwnerEntityInfo();
    }

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
    public string? RoomName { get; set; }
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

public class OwnerEntityInfo : IQueryEntity
{
    public string FullName { get; set; } = default!;
    
    public string Username { get; set; } = default!;
    
    public string Email { get; set; } = default!;
    
    public bool IsEmailVerified { get; set; }
    
    public bool IsDocumentVerified { get; set; }

    public AvatarResponse? Url { get; set; }

    public List<RoleResponse> Roles { get; set; } = new();
    
    public List<string>? Languages { get; set; }
    
    public List<string>? Permissions { get; set; }

    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}

public class AvatarResponse
{
    public string Url { get; set; } = default!;
    
    public int UserId { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public int Id { get; set; }
}

public class RoleResponse
{
    public int Id { get; set; }
    
    public string Name { get; set; } = default!;
}