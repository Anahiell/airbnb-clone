using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.Domain.BoundedContexts.ProductFeatureManagement.Feature.Events;
using Airbnb.Domain.BoundedContexts.ProductFeatureManagement.Feature.Interfaces;
using MediatR;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductFeatureManagement.Commands.DeleteFeatureCommand;

public class DeleteFeatureCommandHandler : ICommandHandler<DeleteFeatureCommand, Result<string>>
{
    private readonly IFeatureRepository _featureRepository;
    private readonly IMediator _mediator;

    public DeleteFeatureCommandHandler(IFeatureRepository featureRepository, IMediator mediator)
    {
        _featureRepository = featureRepository;
        _mediator = mediator;
    }

    public async Task<Result<string>> Handle(DeleteFeatureCommand request, CancellationToken cancellationToken)
    {
        var feature = await _featureRepository.GetByIdAsync(request.Id, cancellationToken);
        if (feature == null)
            return Result<string>.Failure("Характеристика не найдена");

        feature.Delete();

        await _featureRepository.DeleteAsync(feature.Id, cancellationToken);
        await _mediator.Publish(new FeatureDeletedEvent(feature.Id), cancellationToken);

        return Result<string>.Success("Характеристика успешно удалена");
    }
}