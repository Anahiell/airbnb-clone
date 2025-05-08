using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.SharedKernel.Repositories;
using Airbnb.TagsManagement.Domain.BoundedContexts.ProductTagManagement.Aggregates;
using Airbnb.TagsManagement.Domain.BoundedContexts.ProductTagManagement.Events;
using Airbnb.TagsManagement.Domain.BoundedContexts.ProductTagManagement.Interfaces;
using MediatR;

namespace Airbnb.TagsManagement.Application.BoundedContext.ProductTagManagement.Commands.CreateProductTagCommand;

public class CreateProductTagCommandHandler(IProductTagRepository repository, IMediator mediator)
    : ICommandHandler<CreateProductTagCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateProductTagCommand request, CancellationToken cancellationToken)
    {
        var entity = new ProductTag(request.ProductId, request.TagId);

        var result = await repository.AddAsync(entity, cancellationToken);

        await mediator.Publish(new ProductTagCreatedEvent(entity.Id, entity.ProductId, entity.TagId), cancellationToken);

        return Result<int>.Success(result);
    }
}