namespace Airbnb.Infrastructure.Entities;

public class Review
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public int OwnerId { get; set; }
    
    public int ProductId { get; set; }
}