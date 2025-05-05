using Airbnb.MongoRepository.Interfaces;
using Airbnb.PictureManagement.Application.BoundedContext.QueryObjects;
using Airbnb.PictureManagement.Application.BoundedContext.UserPictureManagement.QueryObjects;
using Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Events;
using MediatR;

namespace Airbnb.PictureManagement.Application.BoundedContext.UserPictureManagement.Projections;

public class UserPictureDeletedProjection : INotificationHandler<UserPictureDeletedEvent>
{
    private readonly IProjectionRepository<UserPictureEntityInfo> _repository;

    public UserPictureDeletedProjection(IProjectionRepository<UserPictureEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(UserPictureDeletedEvent @event, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(@event.AggregateId);
    }
}