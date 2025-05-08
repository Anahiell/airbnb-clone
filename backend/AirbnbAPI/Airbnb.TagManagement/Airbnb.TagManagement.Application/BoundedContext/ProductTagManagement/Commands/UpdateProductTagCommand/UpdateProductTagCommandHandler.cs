using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.SharedKernel.Repositories;
using Airbnb.TagsManagement.Domain.BoundedContexts.ProductTagManagement.Aggregates;
using Airbnb.TagsManagement.Domain.BoundedContexts.ProductTagManagement.Events;
using Airbnb.TagsManagement.Domain.BoundedContexts.ProductTagManagement.Interfaces;
using MediatR;

namespace Airbnb.TagsManagement.Application.BoundedContext.ProductTagManagement.Commands.UpdateProductTagCommand;

public class UpdateProductTagCommandHandler(IProductTagRepository repository, IMediator mediator)
    : ICommandHandler<UpdateProductTagCommand, Result>
{
    public async Task<Result> Handle(UpdateProductTagCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null)
        {
            // return Result.Failure("Связь не найдена");
        }

        entity.UpdateProductTag(request.NewProductId, request.NewTagId);

        await repository.UpdateAsync(entity, cancellationToken);

        await mediator.Publish(new ProductTagUpdatedEvent(entity.Id, entity.ProductId, entity.TagId), cancellationToken);

        return Result.Success();
    }
}