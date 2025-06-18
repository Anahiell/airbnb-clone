using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.Domain.BoundedContexts.ProductFeatureManagement.Feature.Events;
using Airbnb.Domain.BoundedContexts.ProductFeatureManagement.Feature.Interfaces;
using MediatR;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductFeatureManagement.Commands.UpdateFeatureCommand;

public class UpdateFeatureCommandHandler : ICommandHandler<UpdateFeatureCommand, Result<string>>
{
    private readonly IFeatureRepository _featureRepository;
    private readonly IMediator _mediator;

    public UpdateFeatureCommandHandler(IFeatureRepository featureRepository, IMediator mediator)
    {
        _featureRepository = featureRepository;
        _mediator = mediator;
    }

    public async Task<Result<string>> Handle(UpdateFeatureCommand request, CancellationToken cancellationToken)
    {
        var feature = await _featureRepository.GetByIdAsync(request.Id, cancellationToken);
        if (feature == null)
            return Result<string>.Failure("Характеристика не найдена");

        feature.Update(request.Name, request.Forcibly, request.Price);

        await _featureRepository.UpdateAsync(feature, cancellationToken);
        await _mediator.Publish(new FeatureUpdatedEvent(feature.Id, feature.Name, feature.Forcibly, feature.Price), cancellationToken);

        return Result<string>.Success("Характеристика успешно обновлена");
    }
}