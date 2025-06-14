using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.MongoRepository.Repositories;
using Airbnb.UserManagement.Application.BoundedContexts.UserPermissionManagement.QueryObjects;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserPermissionManagement.Queries.GetAllUserPermissionsQuery;

public class GetAllUserPermissionsQueryHandler : IQueryHandler<GetAllUserPermissionsQuery, Result<IEnumerable<UserPermissionEntityInfo>>>
{
    private readonly BaseMongoRepository<UserPermissionEntityInfo> _repository;

    public GetAllUserPermissionsQueryHandler(BaseMongoRepository<UserPermissionEntityInfo> repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<UserPermissionEntityInfo>>> Handle(GetAllUserPermissionsQuery request, CancellationToken cancellationToken)
    {
        var all = await _repository.GetAllAsync();
        return Result<IEnumerable<UserPermissionEntityInfo>>.Success(all);
    }
}