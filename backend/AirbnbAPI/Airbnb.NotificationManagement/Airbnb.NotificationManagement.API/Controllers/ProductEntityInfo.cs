using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Controllers;

public class ProductEntityInfo
{
    public string? Title { get; set; }

    public string? Description { get; set; }
    public int Price { get; set; }

    public double Rating { get; set; }
    public int UserId { get; set; }

    public bool Availability { get; set; }

    public int ApartmentTypeId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int AddressLegalId { get; set; }
    
    public string? AddressFull { get; set; }
    public List<ReviewInfo>? Review { get; set; }
    public List<TagInfo>? Tags { get; set; }
    public List<OrderInfo>? Orders { get; set; }
    public List<PictureInfo>? Pictures { get; set; }
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

public enum PropertyTypeEnum
{
    [Display(Name = "Apartment")] Apartment = 1,
    [Display(Name = "House")] House = 2,
    [Display(Name = "Townhouse")] Townhouse = 3,
    [Display(Name = "Loft")] Loft = 4
}