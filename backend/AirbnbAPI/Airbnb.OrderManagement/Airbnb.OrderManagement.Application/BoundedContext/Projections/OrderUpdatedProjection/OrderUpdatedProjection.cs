using Airbnb.MongoRepository.Interfaces;
using Airbnb.OrderManagement.Application.BoundedContext.QueryObjects;
using Airbnb.OrderManagement.Domain.BoundedContexts.OrderManagement.Events;
using MediatR;

namespace Airbnb.OrderManagement.Application.BoundedContext.Projections.OrderUpdatedProjection;

public class OrderUpdatedProjection : INotificationHandler<OrderUpdatedEvent>
{
    private readonly IProjectionRepository<OrderEntityInfo> _repository;

    public OrderUpdatedProjection(IProjectionRepository<OrderEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(OrderUpdatedEvent @event, CancellationToken cancellationToken)
    {
        var updatedOrder = new OrderEntityInfo
        {
            Id = @event.AggregateId,
            ProductId = @event.ProductId,
            UserId = @event.UserId,
            DateStart = @event.DateStart,
            DateEnd = @event.DateEnd
        };

        await _repository.UpdateAsync(updatedOrder);
    }
}