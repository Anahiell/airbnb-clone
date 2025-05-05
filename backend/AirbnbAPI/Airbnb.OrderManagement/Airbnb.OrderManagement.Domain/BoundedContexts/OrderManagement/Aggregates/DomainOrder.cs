using Airbnb.OrderManagement.Domain.BoundedContexts.OrderManagement.Events;
using Airbnb.SharedKernel;

namespace Airbnb.OrderManagement.Domain.BoundedContexts.OrderManagement.Aggregates;

public class DomainOrder : AggregateRoot
{
    public int ProductId { get; private set; }

    public int UserId { get; private set; }

    public DateTime DateStart { get; private set; }

    public DateTime DateEnd { get; private set; }

    public DomainOrder()
    {
    }

    public DomainOrder(int productId, int userId, DateTime dateStart, DateTime dateEnd)
    {
        ProductId = productId;
        UserId = userId;
        DateStart = DateTime.SpecifyKind(dateStart, DateTimeKind.Utc);
        DateEnd = DateTime.SpecifyKind(dateEnd, DateTimeKind.Utc);

        RaiseEvent(new OrderCreatedEvent(Id, productId, userId, dateStart, dateEnd));
    }

    #region Aggregate Methods

    public void CreateOrder(int productId, int userId, DateTime dateStart, DateTime dateEnd)
    {
        ProductId = productId;
        UserId = userId;
        DateStart = dateStart;
        DateEnd = dateEnd;

        RaiseEvent(new OrderCreatedEvent(Id, productId, userId, dateStart, dateEnd));
    }

    public void UpdateOrder(int productId, int userId, DateTime dateStart, DateTime dateEnd)
    {
        ProductId = productId;
        UserId = userId;
        DateStart = dateStart;
        DateEnd = dateEnd;

        RaiseEvent(new OrderUpdatedEvent(Id, productId, userId, dateStart, dateEnd));
    }

    public void DeleteOrder()
    {
        RaiseEvent(new OrderDeletedEvent(Id));
    }

    #endregion

    #region Event Handling

    protected override void When(IDomainEvent @event)
    {
        switch (@event)
        {
            case OrderCreatedEvent e:
                OnOrderCreatedEvent(e);
                break;
            case OrderUpdatedEvent e:
                OnOrderUpdatedEvent(e);
                break;
            case OrderDeletedEvent e:
                OnOrderDeletedEvent(e);
                break;
        }
    }

    private void OnOrderCreatedEvent(OrderCreatedEvent e)
    {
        Id = e.AggregateId;
        ProductId = e.ProductId;
        UserId = e.UserId;
        DateStart = e.DateStart;
        DateEnd = e.DateEnd;
    }

    private void OnOrderUpdatedEvent(OrderUpdatedEvent e)
    {
        Id = e.AggregateId;
        ProductId = e.ProductId;
        UserId = e.UserId;
        DateStart = e.DateStart;
        DateEnd = e.DateEnd;
    }

    private void OnOrderDeletedEvent(OrderDeletedEvent e)
    {
        Id = e.AggregateId;
    }

    #endregion
}
