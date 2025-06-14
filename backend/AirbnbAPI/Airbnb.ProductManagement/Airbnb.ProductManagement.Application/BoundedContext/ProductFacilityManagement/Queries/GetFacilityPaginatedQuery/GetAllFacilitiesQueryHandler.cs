using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.MongoRepository.Repositories;
using Airbnb.ProductManagement.Application.BoundedContext.ProductFacilityManagement.QueryObjects;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductFacilityManagement.Queries.GetFacilityPaginatedQuery;

public class GetAllFacilitiesQueryHandler : IQueryHandler<GetAllFacilitiesQuery, Result<List<FacilityEntityInfo>>>
{
    private readonly BaseMongoRepository<FacilityEntityInfo> _repository;

    public GetAllFacilitiesQueryHandler(BaseMongoRepository<FacilityEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Result<List<FacilityEntityInfo>>> Handle(GetAllFacilitiesQuery request, CancellationToken cancellationToken)
    {
        var facilities = await _repository.GetAllAsync();
        return Result<List<FacilityEntityInfo>>.Success(facilities.ToList());
    }
}