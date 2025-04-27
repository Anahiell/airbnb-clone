using Airbnb.MongoRepository.Interfaces;
using Airbnb.PictureManagement.Application.BoundedContext.QueryObjects;
using Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Events;
using MediatR;

namespace Airbnb.PictureManagement.Application.BoundedContext.Projections.PictureDeletedProjection;


public class PictureDeletedProjection : INotificationHandler<PictureDeletedEvent>
{
    private readonly IProjectionRepository<PictureEntityInfo> _repository;

    public PictureDeletedProjection(IProjectionRepository<PictureEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(PictureDeletedEvent @event, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(@event.AggregateId);
    }
}