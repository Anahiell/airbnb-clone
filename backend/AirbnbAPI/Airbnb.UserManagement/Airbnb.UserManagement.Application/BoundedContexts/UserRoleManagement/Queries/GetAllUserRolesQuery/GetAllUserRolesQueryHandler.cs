using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.MongoRepository.Repositories;
using Airbnb.UserManagement.Application.BoundedContexts.UserRoleManagement.QueryObjects;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserRoleManagement.Queries.GetAllUserRolesQuery;

public class GetAllUserRolesQueryHandler : IQueryHandler<GetAllUserRolesQuery, Result<IEnumerable<UserRoleEntityInfo>>>
{
    private readonly BaseMongoRepository<UserRoleEntityInfo> _repository;

    public GetAllUserRolesQueryHandler(BaseMongoRepository<UserRoleEntityInfo> repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<UserRoleEntityInfo>>> Handle(GetAllUserRolesQuery request, CancellationToken cancellationToken)
    {
        var items = await _repository.GetAllAsync();
        return Result<IEnumerable<UserRoleEntityInfo>>.Success(items);
    }
}