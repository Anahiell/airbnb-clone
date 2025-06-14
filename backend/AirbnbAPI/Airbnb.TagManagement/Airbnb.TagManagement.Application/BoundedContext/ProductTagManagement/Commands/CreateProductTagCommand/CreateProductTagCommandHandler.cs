using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.SharedKernel.Repositories;
using Airbnb.TagsManagement.Application.BoundedContext.ProductTagManagement.ProductTagUpdatedConsumer.Publisher;
using Airbnb.TagsManagement.Domain.BoundedContexts.ProductTagManagement.Aggregates;
using Airbnb.TagsManagement.Domain.BoundedContexts.ProductTagManagement.Events;
using Airbnb.TagsManagement.Domain.BoundedContexts.ProductTagManagement.Interfaces;
using Airbnb.TagsManagement.Domain.BoundedContexts.TagsManagement.Aggregates;
using Airbnb.TagsManagement.Domain.BoundedContexts.TagsManagement.Interfaces;
using MassTransit;
using MediatR;

namespace Airbnb.TagsManagement.Application.BoundedContext.ProductTagManagement.Commands.CreateProductTagCommand;

public class CreateProductTagCommandHandler(IProductTagRepository repository, IRepository<DomainTag> tagRepository,
    ITagEventDispatcher tagEventDispatcher, IMediator mediator)
    : ICommandHandler<CreateProductTagCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateProductTagCommand request, CancellationToken cancellationToken)
    {
        var entity = new ProductTag(request.ProductId, request.TagId);

        var result = await repository.AddAsync(entity, cancellationToken);
        
        var tag = await tagRepository.GetByIdAsync(request.TagId, cancellationToken);
        if (tag is null)
        {
            return Result<int>.Failure("Tag not found");
        }

        await mediator.Publish(new ProductTagCreatedEvent(entity.Id, entity.ProductId, entity.TagId), cancellationToken);

        await tagEventDispatcher.DispatchAsync(new ProductManagement.Application.BoundedContext.Events.ProductEvent.ProductTag.ProductTagUpdatedEvent(entity.ProductId, entity.TagId, tag.Name), cancellationToken);
        
        return Result<int>.Success(result);
    }
}