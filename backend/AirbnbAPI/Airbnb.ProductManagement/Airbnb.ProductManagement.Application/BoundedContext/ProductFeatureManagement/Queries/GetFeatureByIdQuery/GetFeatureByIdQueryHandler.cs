using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.MongoRepository.Repositories;
using Airbnb.ProductManagement.Application.BoundedContext.ProductFeatureManagement.QueryObjects;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductFacilityManagement.Queries.GetFacilityPaginatedQuery;

public class GetFeatureByIdQueryHandler : IQueryHandler<GetFeatureByIdQuery, Result<FeatureEntityInfo>>
{
    private readonly BaseMongoRepository<FeatureEntityInfo> _repository;

    public GetFeatureByIdQueryHandler(BaseMongoRepository<FeatureEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Result<FeatureEntityInfo>> Handle(GetFeatureByIdQuery request, CancellationToken cancellationToken)
    {
        var feature = await _repository.FindByIdAsync(request.FeatureId);

        if (feature is null)
            return Result<FeatureEntityInfo>.Failure("Feature не найден");

        return Result<FeatureEntityInfo>.Success(feature);
    }
}