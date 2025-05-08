using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.SharedKernel.Repositories;
using Airbnb.TagsManagement.Domain.BoundedContexts.TagsManagement.Aggregates;
using Airbnb.TagsManagement.Domain.BoundedContexts.TagsManagement.Events;
using MediatR;

namespace Airbnb.TagsManagement.Application.BoundedContext.Commands.DeleteTag;

public class DeleteTagCommandHandler(IRepository<DomainTag> tagRepository, IMediator mediator)
    : ICommandHandler<DeleteTagCommand, Result>
{
    public async Task<Result> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        var tag = await tagRepository.GetByIdAsync(request.Id, cancellationToken);
        if (tag is null)
            // return Result.Failure("Тег не найден");

        await tagRepository.DeleteAsync(request.Id, cancellationToken);

        await mediator.Publish(new TagDeletedEvent(tag.Id), cancellationToken);

        return Result.Success();
    }
}