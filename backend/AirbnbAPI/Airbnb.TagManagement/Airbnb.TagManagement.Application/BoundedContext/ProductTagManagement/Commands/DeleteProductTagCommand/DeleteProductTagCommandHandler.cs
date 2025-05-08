using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.SharedKernel.Repositories;
using Airbnb.TagsManagement.Domain.BoundedContexts.ProductTagManagement.Aggregates;
using Airbnb.TagsManagement.Domain.BoundedContexts.ProductTagManagement.Events;
using Airbnb.TagsManagement.Domain.BoundedContexts.ProductTagManagement.Interfaces;
using MediatR;

namespace Airbnb.TagsManagement.Application.BoundedContext.ProductTagManagement.Commands.DeleteProductTagCommand;

public class DeleteProductTagCommandHandler(IProductTagRepository repository, IMediator mediator)
    : ICommandHandler<DeleteProductTagCommand, Result>
{
    public async Task<Result> Handle(DeleteProductTagCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null)
        {
            // return Result.Failure("Связь не найдена");
        }

        await repository.DeleteAsync(request.Id, cancellationToken);

        await mediator.Publish(new ProductTagDeletedEvent(entity.Id), cancellationToken);

        return Result.Success();
    }
}