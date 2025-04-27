using Airbnb.MongoRepository.Interfaces;
using Airbnb.OrderManagement.Application.BoundedContext.QueryObjects;
using Airbnb.OrderManagement.Domain.BoundedContexts.OrderManagement.Events;
using MediatR;

namespace Airbnb.OrderManagement.Application.BoundedContext.Projections.OrderDeletedProjection;

public class OrderDeletedProjection : INotificationHandler<OrderDeletedEvent>
{
    private readonly IProjectionRepository<OrderEntityInfo> _repository;

    public OrderDeletedProjection(IProjectionRepository<OrderEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(OrderDeletedEvent @event, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(@event.AggregateId);
    }
}