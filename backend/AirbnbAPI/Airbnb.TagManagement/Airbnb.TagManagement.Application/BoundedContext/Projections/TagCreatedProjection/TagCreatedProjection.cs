using Airbnb.MongoRepository.Interfaces;
using Airbnb.TagsManagement.Application.BoundedContext.QueryObjects;
using Airbnb.TagsManagement.Domain.BoundedContexts.TagsManagement.Events;
using MediatR;

namespace Airbnb.TagsManagement.Application.BoundedContext.Projections.TagCreatedProjection;

public class TagCreatedProjection : INotificationHandler<TagCreatedEvent>
{
    private readonly IProjectionRepository<TagEntityInfo> _repository;

    public TagCreatedProjection(IProjectionRepository<TagEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task Handle(TagCreatedEvent @event, CancellationToken cancellationToken)
    {
        var tag = new TagEntityInfo
        {
            Id = @event.AggregateId,
            Name = @event.Name
        };

        await _repository.InsertAsync(tag); // CancellationToken потом 
    }
}