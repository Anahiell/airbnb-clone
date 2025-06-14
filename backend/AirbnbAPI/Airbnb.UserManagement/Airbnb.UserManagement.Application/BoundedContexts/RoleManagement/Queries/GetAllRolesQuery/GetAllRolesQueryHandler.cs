using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.MongoRepository.Repositories;
using Airbnb.UserManagement.Application.BoundedContexts.RoleManagement.QueryObjects;

namespace Airbnb.UserManagement.Application.BoundedContexts.RoleManagement.Queries.GetAllRolesQuery;

public class GetAllRolesQueryHandler : IQueryHandler<GetAllRolesQuery, Result<IEnumerable<RoleEntityInfo>>>
{
    private readonly BaseMongoRepository<RoleEntityInfo> _repository;

    public GetAllRolesQueryHandler(BaseMongoRepository<RoleEntityInfo> repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<RoleEntityInfo>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await _repository.GetAllAsync();
        return Result<IEnumerable<RoleEntityInfo>>.Success(roles);
    }
}