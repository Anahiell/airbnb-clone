using Airbnb.MongoRepository.Interfaces;
using Airbnb.PictureManagement.Application.BoundedContext.QueryObjects;
using Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Events;
using Airbnb.PictureManagement.Domain.BoundedContexts.ProductPictureManagement.Events;
using MediatR;

namespace Airbnb.PictureManagement.Application.BoundedContext.Projections.PictureUpdatedProjection;

public class PictureUpdatedProjection : INotificationHandler<ProductPictureUpdatedEvent>
{
    private readonly IProjectionRepository<PictureEntityInfo> _repository;

    public PictureUpdatedProjection(IProjectionRepository<PictureEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(ProductPictureUpdatedEvent @event, CancellationToken cancellationToken)
    {
        var updatedPicture = new PictureEntityInfo
        {
            Id = @event.Id,
            ProductId = @event.ProductId,
            Url = @event.Url
        };

        await _repository.UpdateAsync(updatedPicture);
    }
}