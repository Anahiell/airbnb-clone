using Airbnb.MongoRepository.Interfaces;
using Airbnb.TagsManagement.Application.BoundedContext.ProductTagManagement.QueryObjects;
using Airbnb.TagsManagement.Domain.BoundedContexts.ProductTagManagement.Events;
using MediatR;

namespace Airbnb.TagsManagement.Application.BoundedContext.ProductTagManagement.Projections.ProductTagUpdatedProjection;

public class ProductTagUpdatedProjection : INotificationHandler<ProductTagUpdatedEvent>
{
    private readonly IProjectionRepository<ProductTagEntityInfo> _repository;

    public ProductTagUpdatedProjection(IProjectionRepository<ProductTagEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(ProductTagUpdatedEvent @event, CancellationToken cancellationToken)
    {
        var updated = new ProductTagEntityInfo
        {
            Id = @event.AggregateId,
            ProductId = @event.NewProductId,
            TagId = @event.NewTagId
        };

        await _repository.UpsertAsync(updated);
    }
}