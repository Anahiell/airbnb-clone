using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.SharedKernel.Repositories;
using Airbnb.TagsManagement.Domain.BoundedContexts.TagsManagement.Aggregates;
using Airbnb.TagsManagement.Domain.BoundedContexts.TagsManagement.Events;
using Airbnb.TagsManagement.Domain.BoundedContexts.TagsManagement.Interfaces;
using MediatR;

namespace Airbnb.TagsManagement.Application.BoundedContext.Commands.CreateTag;

public class CreateTagCommandHandler(IRepository<DomainTag> tagRepository, IMediator mediator)
    : ICommandHandler<CreateTagCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var tag = new DomainTag(request.Name);

        var result = await tagRepository.AddAsync(tag, cancellationToken);

        await mediator.Publish(new TagCreatedEvent(tag.Id, tag.Name, tag.CreatedAt), cancellationToken);

        return Result<int>.Success(result);
    }
}