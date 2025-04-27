using Airbnb.MongoRepository.Interfaces;
using Airbnb.OrderManagement.Application.BoundedContext.QueryObjects;
using Airbnb.OrderManagement.Domain.BoundedContexts.OrderManagement.Events;
using MediatR;

namespace Airbnb.OrderManagement.Application.BoundedContext.Projections.OrderCreatedProjection;

public class OrderCreatedProjection : INotificationHandler<OrderCreatedEvent>
{
    private readonly IProjectionRepository<OrderEntityInfo> _repository;

    public OrderCreatedProjection(IProjectionRepository<OrderEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(OrderCreatedEvent @event, CancellationToken cancellationToken)
    {
        var order = new OrderEntityInfo
        {
            Id = @event.AggregateId,
            ProductId = @event.ProductId,
            UserId = @event.UserId,
            DateStart = @event.DateStart,
            DateEnd = @event.DateEnd
        };

        await _repository.InsertAsync(order);
    }
}