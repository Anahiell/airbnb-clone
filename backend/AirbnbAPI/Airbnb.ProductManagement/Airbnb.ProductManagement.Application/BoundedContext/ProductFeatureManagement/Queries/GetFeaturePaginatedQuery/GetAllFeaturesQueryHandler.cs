using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.MongoRepository.Repositories;
using Airbnb.ProductManagement.Application.BoundedContext.ProductFeatureManagement.QueryObjects;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductFeatureManagement.Queries.GetFeaturePaginatedQuery;

public class GetAllFeaturesQueryHandler : IQueryHandler<GetAllFeaturesQuery, Result<List<FeatureEntityInfo>>>
{
    private readonly BaseMongoRepository<FeatureEntityInfo> _repository;

    public GetAllFeaturesQueryHandler(BaseMongoRepository<FeatureEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Result<List<FeatureEntityInfo>>> Handle(GetAllFeaturesQuery request, CancellationToken cancellationToken)
    {
        var features = await _repository.GetAllAsync();
        return Result<List<FeatureEntityInfo>>.Success(features.ToList());
    }
}