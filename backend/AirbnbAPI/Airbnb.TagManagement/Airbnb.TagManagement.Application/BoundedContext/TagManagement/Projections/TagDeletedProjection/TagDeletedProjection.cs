using Airbnb.MongoRepository.Interfaces;
using Airbnb.TagsManagement.Application.BoundedContext.QueryObjects;
using Airbnb.TagsManagement.Domain.BoundedContexts.TagsManagement.Events;
using MediatR;

namespace Airbnb.TagsManagement.Application.BoundedContext.Projections.TagDeletedProjection;

public class TagDeletedProjection : INotificationHandler<TagDeletedEvent>
{
    private readonly IProjectionRepository<TagEntityInfo> _repository;

    public TagDeletedProjection(IProjectionRepository<TagEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(TagDeletedEvent @event, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(@event.AggregateId);
    }
}