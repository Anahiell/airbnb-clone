using Airbnb.Application.Results;
using Airbnb.Application.UseCases;
using Airbnb.Domain.BoundedContexts.ProductFeatureManagement.Feature.Interfaces;
using Airbnb.Domain.BoundedContexts.ProductFeatureManagement.ProductFeature.Aggregates;
using Airbnb.Domain.BoundedContexts.ProductFeatureManagement.ProductFeature.Events;
using Airbnb.Domain.BoundedContexts.ProductFeatureManagement.ProductFeature.Interfaces;
using MediatR;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductManagement.UseCases.Feature;

public record UpdateProductFeaturesUseCase(int ProductId, IEnumerable<string>? FeatureNames) : IUseCase<Result<string>>;

public class UpdateProductFeaturesUseCaseHandler : IUseCaseHandler<UpdateProductFeaturesUseCase, Result<string>>
{
    private readonly IFeatureRepository _featureRepository;
    private readonly IProductFeatureRepository _productFeatureRepository;
    private readonly IMediator _mediator;

    public UpdateProductFeaturesUseCaseHandler(IFeatureRepository featureRepository,
        IProductFeatureRepository productFeatureRepository,
        IMediator mediator)
    {
        _featureRepository = featureRepository;
        _productFeatureRepository = productFeatureRepository;
        _mediator = mediator;
    }

    public async Task<Result<string>> Handle(UpdateProductFeaturesUseCase request, CancellationToken cancellationToken)
    {
        var featureNames = request.FeatureNames?.Distinct(StringComparer.OrdinalIgnoreCase).ToList() ?? new();

        var features = new List<Domain.BoundedContexts.ProductFeatureManagement.Feature.Aggregates.Feature>();
        foreach (var name in featureNames)
        {
            var f = await _featureRepository.GetByNameAsync(name, cancellationToken);
            if (f is not null)
                features.Add(f);
        }

        var missing = featureNames.Except(features.Select(f => f.Name), StringComparer.OrdinalIgnoreCase).ToList();
        if (missing.Any())
            return Result<string>.Failure($"Фичи не найдены: {string.Join(", ", missing)}");

        await _productFeatureRepository.DeleteAllByProductIdAsync(request.ProductId, cancellationToken);

        var createdFeatures = new List<(int Id, string Name, bool Forcibility, int Price)>();
        foreach (var feature in features)
        {
            var pf = new ProductFeature(request.ProductId, feature.Id);
            await _productFeatureRepository.AddAsync(pf, cancellationToken);

            createdFeatures.Add((feature.Id, feature.Name, feature.Forcibly, feature.Price));
        }

        if (createdFeatures.Count > 0)
        {
            var evt = new ProductFeaturesUpdatedEvent(request.ProductId, createdFeatures);
            await _mediator.Publish(evt, cancellationToken);
        }

        return Result<string>.Success("Фичи успешно обновлены");
    }
}