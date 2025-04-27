using Airbnb.MongoRepository.Interfaces;
using Airbnb.PictureManagement.Application.BoundedContext.QueryObjects;
using Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Events;
using MediatR;

namespace Airbnb.PictureManagement.Application.BoundedContext.Projections.PictureUpdatedProjection;

public class PictureUpdatedProjection : INotificationHandler<PictureUpdatedEvent>
{
    private readonly IProjectionRepository<PictureEntityInfo> _repository;

    public PictureUpdatedProjection(IProjectionRepository<PictureEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(PictureUpdatedEvent @event, CancellationToken cancellationToken)
    {
        var updatedPicture = new PictureEntityInfo
        {
            Id = @event.AggregateId,
            Url = @event.Url
        };

        await _repository.UpdateAsync(updatedPicture);
    }
}