using Airbnb.MongoRepository.Interfaces;
using Airbnb.TagsManagement.Application.BoundedContext.ProductTagManagement.QueryObjects;
using Airbnb.TagsManagement.Domain.BoundedContexts.ProductTagManagement.Events;
using MediatR;

namespace Airbnb.TagsManagement.Application.BoundedContext.ProductTagManagement.Projections.ProductTagDeletedProjection;

public class ProductTagDeletedProjection : INotificationHandler<ProductTagDeletedEvent>
{
    private readonly IProjectionRepository<ProductTagEntityInfo> _repository;

    public ProductTagDeletedProjection(IProjectionRepository<ProductTagEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(ProductTagDeletedEvent @event, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(@event.AggregateId);
    }
}