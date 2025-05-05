using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.SharedKernel.Repositories;
using Airbnb.TagsManagement.Domain.BoundedContexts.TagsManagement.Aggregates;
using Airbnb.TagsManagement.Domain.BoundedContexts.TagsManagement.Events;
using Airbnb.TagsManagement.Domain.BoundedContexts.TagsManagement.Interfaces;
using MediatR;

namespace Airbnb.TagsManagement.Application.BoundedContext.Commands.UpdateTag;

public class UpdateTagCommandHandler(IRepository<DomainTag> tagRepository, IMediator mediator)
    : ICommandHandler<UpdateTagCommand, Result>
{
    public async Task<Result> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
    {
        var tag = await tagRepository.GetByIdAsync(request.Id, cancellationToken);
        if (tag is null)
        {
            // return Result.Failure("Тег не найден");   
        }

        tag.UpdateName(request.NewName);

        await tagRepository.UpdateAsync(tag, cancellationToken);

        await mediator.Publish(new TagUpdatedEvent(tag.Id, tag.Name), cancellationToken);

        return Result.Success();
    }
}