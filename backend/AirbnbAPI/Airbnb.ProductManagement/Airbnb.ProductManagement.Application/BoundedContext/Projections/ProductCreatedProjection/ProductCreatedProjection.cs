using Airbnb.Domain.BoundedContexts.ProductManagement.Events;
using Airbnb.MongoRepository.Interfaces;
using Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;
using MediatR;

namespace Airbnb.ProductManagement.Application.BoundedContext.Projections;

public class ProductCreatedProjection : INotificationHandler<ProductCreatedEvent>
{
    private readonly IProjectionRepository<ProductEntityInfo> _repository;

    public ProductCreatedProjection(IProjectionRepository<ProductEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(ProductCreatedEvent @event, CancellationToken cancellationToken)
    {
        var product = new ProductEntityInfo
        {
            Id = @event.AggregateId,
            Title = @event.Title,
            Description = @event.Description,
            CreatedDate = @event.CreatedDate,
            Availability = @event.IsAvailable,
            Price = @event.Price,
            UserId = @event.UserId,
            AddressLegalId = @event.AddressLegalId,
            ApartmentTypeId = @event.AppartmentTypeId,
        };

        await _repository.InsertAsync(product);
    }
}
