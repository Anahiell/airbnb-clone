using Airbnb.Application.UseCases;
using Airbnb.Domain.BoundedContexts.ProductManagement.Events;
using Airbnb.MongoRepository.Interfaces;
using Airbnb.ProductManagement.Application.BoundedContext.Commands.CreateProduct;
using Airbnb.ProductManagement.Application.BoundedContext.ProductManagement.UseCases.User;
using Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;
using MediatR;

namespace Airbnb.ProductManagement.Application.BoundedContext.Projections;

public class ProductCreatedProjection : INotificationHandler<ProductCreatedEvent>
{
    private readonly IProjectionRepository<ProductEntityInfo> _repository;
    private readonly IUseCaseDispatcher useCaseDispatcher;
    public ProductCreatedProjection(IProjectionRepository<ProductEntityInfo> repository, IUseCaseDispatcher useCaseDispatcher)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        this.useCaseDispatcher = useCaseDispatcher;
    }

    public async Task Handle(ProductCreatedEvent @event, CancellationToken cancellationToken)
    {
        var user = await useCaseDispatcher.DispatchAsync(new GetUserByIdUseCase(@event.UserId),
            cancellationToken);
        
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
            Owner = user,
        };

        await _repository.InsertAsync(product);
    }
}