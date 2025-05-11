namespace Airbnb.ProductManagement.Application.BoundedContext.Events;

public class ProductSignalRCreatedEvent
{
    public int ProductId { get; set; }
    public string ProductTitle { get; set; }
    public string ProductDescription { get; set; }
    public decimal ProductPrice { get; set; }
    public bool IsActive { get; set; }
}

public class ProductSignalRUpdatedEvent
{
    public int ProductId { get; set; }
    public string ProductTitle { get; set; }
    public string ProductDescription { get; set; }
    public decimal ProductPrice { get; set; }
    public bool IsActive { get; set; }
}

