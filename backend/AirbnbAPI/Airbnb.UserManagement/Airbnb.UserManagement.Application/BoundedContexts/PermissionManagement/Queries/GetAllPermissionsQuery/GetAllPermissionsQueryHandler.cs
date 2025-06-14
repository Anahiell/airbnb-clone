using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.MongoRepository.Repositories;
using Airbnb.UserManagement.Application.BoundedContexts.PermissionManagement.QueryObjects;

namespace Airbnb.UserManagement.Application.BoundedContexts.PermissionManagement.Queries.GetAllPermissionsQuery;

public class GetAllPermissionsQueryHandler : IQueryHandler<GetAllPermissionsQuery, Result<IEnumerable<PermissionEntityInfo>>>
{
    private readonly BaseMongoRepository<PermissionEntityInfo> _repository;

    public GetAllPermissionsQueryHandler(BaseMongoRepository<PermissionEntityInfo> repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<PermissionEntityInfo>>> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken)
    {
        var allPermissions = await _repository.GetAllAsync();
        return Result<IEnumerable<PermissionEntityInfo>>.Success(allPermissions);
    }
}