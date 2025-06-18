using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.Domain.BoundedContexts.ProductFeatureManagement.Feature.Aggregates;
using Airbnb.Domain.BoundedContexts.ProductFeatureManagement.Feature.Events;
using Airbnb.Domain.BoundedContexts.ProductFeatureManagement.Feature.Interfaces;
using MediatR;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductFeatureManagement.Commands.CreateFeatureCommand;

public class CreateFeatureCommandHandler : ICommandHandler<CreateFeatureCommand, Result<int>>
{
    private readonly IFeatureRepository _featureRepository;
    private readonly IMediator _mediator;

    public CreateFeatureCommandHandler(IFeatureRepository featureRepository, IMediator mediator)
    {
        _featureRepository = featureRepository;
        _mediator = mediator;
    }

    public async Task<Result<int>> Handle(CreateFeatureCommand request, CancellationToken cancellationToken)
    {
        var feature = new Feature(request.Name, request.Forcibly, request.Price);
        var result = await _featureRepository.AddAsync(feature, cancellationToken);

        await _mediator.Publish(new FeatureCreatedEvent(feature.Id, feature.Name, feature.Forcibly, feature.Price), cancellationToken);

        return Result<int>.Success(result);
    }
}