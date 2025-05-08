using Airbnb.MongoRepository.Interfaces;
using Airbnb.TagsManagement.Application.BoundedContext.ProductTagManagement.QueryObjects;
using Airbnb.TagsManagement.Domain.BoundedContexts.ProductTagManagement.Events;
using MediatR;

namespace Airbnb.TagsManagement.Application.BoundedContext.ProductTagManagement.Projections.ProductTagCreatedProjection;

public class ProductTagCreatedProjection : INotificationHandler<ProductTagCreatedEvent>
{
    private readonly IProjectionRepository<ProductTagEntityInfo> _repository;

    public ProductTagCreatedProjection(IProjectionRepository<ProductTagEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(ProductTagCreatedEvent @event, CancellationToken cancellationToken)
    {
        var projection = new ProductTagEntityInfo
        {
            Id = @event.AggregateId,
            ProductId = @event.ProductId,
            TagId = @event.TagId
        };

        await _repository.InsertAsync(projection);
    }
}