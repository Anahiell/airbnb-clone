using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.MongoRepository.Repositories;
using Airbnb.UserManagement.Application.BoundedContexts.RoleManagement.QueryObjects;

namespace Airbnb.UserManagement.Application.BoundedContexts.RoleManagement.Queries.GetRoleByIdQuery;

public class GetRoleByIdQueryHandler : IQueryHandler<GetRoleByIdQuery, Result<RoleEntityInfo>>
{
    private readonly BaseMongoRepository<RoleEntityInfo> _repository;

    public GetRoleByIdQueryHandler(BaseMongoRepository<RoleEntityInfo> repository)
    {
        _repository = repository;
    }

    public async Task<Result<RoleEntityInfo>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await _repository.FindByIdAsync(request.Id);

        return role is null
            ? Result<RoleEntityInfo>.Failure("Роль не найдена")
            : Result<RoleEntityInfo>.Success(role);
    }
}