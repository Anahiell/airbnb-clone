using Airbnb.MongoRepository.Interfaces;
using Airbnb.PictureManagement.Application.BoundedContext.QueryObjects;
using Airbnb.PictureManagement.Domain.BoundedContexts.PictureManagement.Events;
using MediatR;

namespace Airbnb.PictureManagement.Application.BoundedContext.Projections.PictureCreatedProjection;

public class PictureCreatedProjection : INotificationHandler<PictureCreatedEvent>
{
    private readonly IProjectionRepository<PictureEntityInfo> _repository;

    public PictureCreatedProjection(IProjectionRepository<PictureEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(PictureCreatedEvent @event, CancellationToken cancellationToken)
    {
        var picture = new PictureEntityInfo
        {
            Id = @event.AggregateId,
            Url = @event.Url,
            UserId = @event.UserId,
            CreatedAt = @event.CreatedDate
        };

        await _repository.InsertAsync(picture);
    }
}