using Airbnb.MongoRepository.Interfaces;
using Airbnb.PictureManagement.Application.BoundedContext.QueryObjects;
using Airbnb.PictureManagement.Application.BoundedContext.UserPictureManagement.QueryObjects;
using Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Events;
using MediatR;

namespace Airbnb.PictureManagement.Application.BoundedContext.UserPictureManagement.Projections;

public class UserPictureCreatedProjection: INotificationHandler<UserPictureCreatedEvent>
{
    private readonly IProjectionRepository<UserPictureEntityInfo> _repository;

    public UserPictureCreatedProjection(IProjectionRepository<UserPictureEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(UserPictureCreatedEvent @event, CancellationToken cancellationToken)
    {
        var picture = new UserPictureEntityInfo
        {
            Id = @event.AggregateId,
            Url = @event.Url,
            UserId = @event.UserId,
            CreatedAt = @event.CreatedDate
        };

        await _repository.InsertAsync(picture);
    }
}