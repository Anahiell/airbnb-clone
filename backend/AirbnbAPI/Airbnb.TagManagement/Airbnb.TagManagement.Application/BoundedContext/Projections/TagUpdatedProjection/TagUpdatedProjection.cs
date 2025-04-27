using Airbnb.MongoRepository.Interfaces;
using Airbnb.TagsManagement.Application.BoundedContext.QueryObjects;
using Airbnb.TagsManagement.Domain.BoundedContexts.TagsManagement.Events;
using MediatR;

namespace Airbnb.TagsManagement.Application.BoundedContext.Projections.TagUpdatedProjection;

public class TagUpdatedProjection : INotificationHandler<TagUpdatedEvent>
{
    private readonly IProjectionRepository<TagEntityInfo> _repository;

    public TagUpdatedProjection(IProjectionRepository<TagEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(TagUpdatedEvent @event, CancellationToken cancellationToken)
    {
        var updatedTag = new TagEntityInfo
        {
            Id = @event.AggregateId,
            Name = @event.NewName
        };

        await _repository.UpdateAsync(updatedTag);
    }
}