namespace Airbnb.Infrastructure.Entities;

public class Order
{
    public int Id { get; set; }
    
    public int PrdouctId { get; set; }
    
    public int UserId { get; set; }
    
    public DateTime DateStart { get; set; }
    
    public DateTime DateEnd { get; set; }
}