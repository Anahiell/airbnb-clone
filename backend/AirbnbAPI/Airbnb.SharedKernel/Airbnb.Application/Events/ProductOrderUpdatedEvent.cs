using MediatR;

namespace Airbnb.ProductManagement.Application.BoundedContext.Events;

public class ProductOrderUpdatedEvent : INotification
{
    public int ProductId { get; set; }
    public int UserId { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
    public int Id { get; set; }
}