namespace Airbnb.Infrastructure.Entities;

public class PropertyEntity
{
    public int Id { get; set; }
    public int OwnerId { get; set; }  //связь с пользователем
    public string Title { get;set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Location { get; set; }
}