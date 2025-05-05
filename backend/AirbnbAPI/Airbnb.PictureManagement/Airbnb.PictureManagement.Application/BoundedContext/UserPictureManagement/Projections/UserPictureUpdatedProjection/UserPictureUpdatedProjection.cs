using Airbnb.MongoRepository.Interfaces;
using Airbnb.PictureManagement.Application.BoundedContext.QueryObjects;
using Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Events;
using MediatR;

namespace Airbnb.PictureManagement.Application.BoundedContext.UserPictureManagement.Projections;

public class UserPictureUpdatedProjection : INotificationHandler<UserPictureUpdatedEvent>
{
    private readonly IProjectionRepository<PictureEntityInfo> _repository;

    public UserPictureUpdatedProjection(IProjectionRepository<PictureEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(UserPictureUpdatedEvent @event, CancellationToken cancellationToken)
    {
        var updatedPicture = new PictureEntityInfo
        {
            Id = @event.AggregateId,
            Url = @event.Url
        };

        // Обновляем картинку в проекции
        await _repository.UpdateAsync(updatedPicture);
    }
}