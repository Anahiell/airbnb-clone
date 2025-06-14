using Airbnb.Domain.BoundedContexts.ProductManagement.Events;
using Airbnb.MongoRepository.Interfaces;
using Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;
using MediatR;

namespace Airbnb.ProductManagement.Application.BoundedContext.Projections.ProductDeletedProjection;

public class ProductDeletedProjection : INotificationHandler<ProductDeletedEvent>
{
    private readonly IProjectionRepository<ProductEntityInfo> _repository;

    public ProductDeletedProjection(IProjectionRepository<ProductEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(ProductDeletedEvent @event, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(@event.AggregateId);
    }
}