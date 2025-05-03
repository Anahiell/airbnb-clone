using Airbnb.Domain.BoundedContexts.ProductManagement.Events;
using Airbnb.MongoRepository.Interfaces;
using Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;
using MediatR;

namespace Airbnb.ProductManagement.Application.BoundedContext.Projections.ProductUpdatedProjection;

public class ProductUpdatedProjection : INotificationHandler<ProductUpdatedEvent>
{
    private readonly IProjectionRepository<ProductEntityInfo> _repository;

    public ProductUpdatedProjection(IProjectionRepository<ProductEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(ProductUpdatedEvent @event, CancellationToken cancellationToken)
    {
        var updatedProduct = new ProductEntityInfo
        {
            Id = @event.AggregateId,
            Title = @event.Title,
            Description = @event.Description,
            Price = @event.Price,
            // Rating = @event.Rating,
            Availability = @event.IsAvailable,
            CreatedDate = @event.CreatedDate,
            UserId = @event.UserId,
            ApartmentTypeId = @event.AppartmentTypeId,
        };

        await _repository.UpdateAsync(updatedProduct);
    }
}