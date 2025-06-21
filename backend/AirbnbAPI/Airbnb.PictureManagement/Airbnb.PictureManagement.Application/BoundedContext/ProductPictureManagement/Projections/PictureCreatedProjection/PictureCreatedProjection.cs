using Airbnb.MongoRepository.Interfaces;
using Airbnb.PictureManagement.Application.BoundedContext.QueryObjects;
using Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Events;
using Airbnb.PictureManagement.Domain.BoundedContexts.ProductPictureManagement.Events;
using MassTransit;
using MediatR;
using ProductPictureUpdatedEvent = Airbnb.ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductPicture.ProductPictureUpdatedEvent;

namespace Airbnb.PictureManagement.Application.BoundedContext.Projections.PictureCreatedProjection;

public class PictureCreatedProjection : INotificationHandler<ProductPictureCreatedEvent>
{
    private readonly IProjectionRepository<PictureEntityInfo> _repository;
    private readonly IBus _bus;
    public PictureCreatedProjection(IProjectionRepository<PictureEntityInfo> repository, IBus bus)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _bus = bus;
    }

    public async Task Handle(ProductPictureCreatedEvent @event, CancellationToken cancellationToken)
    {
        var picture = new PictureEntityInfo
        {
            Id = @event.Id,
            ProductId = @event.ProductId,
            Url = @event.Url,
            CreatedAt = @event.CreatedDate,
            RoomName = @event.RoomName,
        };
        var productPictureUpdatedEvent = new ProductPictureUpdatedEvent(
            ProductId: @event.ProductId,
            PictureId: @event.Id,
            Url: @event.Url,
            false,
            UpdatedAt: @event.CreatedDate,
            RoomId: @event.RoomId,
            RoomName: @event.RoomName
        );
        await _bus.Publish(productPictureUpdatedEvent, cancellationToken);

        await _repository.InsertAsync(picture);
    }
}