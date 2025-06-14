using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.MongoRepository.Repositories;
using Airbnb.ProductManagement.Application.BoundedContext.ProductFacilityManagement.QueryObjects;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductFacilityManagement.Queries.GetFacilityByIdQuery;

public class GetFacilityByIdQueryHandler : IQueryHandler<GetFacilityByIdQuery, Result<FacilityEntityInfo>>
{
    private readonly BaseMongoRepository<FacilityEntityInfo> _repository;

    public GetFacilityByIdQueryHandler(BaseMongoRepository<FacilityEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Result<FacilityEntityInfo>> Handle(GetFacilityByIdQuery request, CancellationToken cancellationToken)
    {
        var facility = await _repository.FindByIdAsync(request.FacilityId);

        if (facility is null)
            return Result<FacilityEntityInfo>.Failure("Facility не найден");

        return Result<FacilityEntityInfo>.Success(facility);
    }
}