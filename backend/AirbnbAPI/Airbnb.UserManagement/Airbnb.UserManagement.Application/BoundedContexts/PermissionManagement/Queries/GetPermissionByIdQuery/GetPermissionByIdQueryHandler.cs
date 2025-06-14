using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.MongoRepository.Repositories;
using Airbnb.UserManagement.Application.BoundedContexts.PermissionManagement.QueryObjects;

namespace Airbnb.UserManagement.Application.BoundedContexts.PermissionManagement.Queries;

public class GetPermissionByIdQueryHandler : IQueryHandler<GetPermissionByIdQuery, Result<PermissionEntityInfo>>
{
    private readonly BaseMongoRepository<PermissionEntityInfo> _repository;

    public GetPermissionByIdQueryHandler(BaseMongoRepository<PermissionEntityInfo> repository)
    {
        _repository = repository;
    }

    public async Task<Result<PermissionEntityInfo>> Handle(GetPermissionByIdQuery request, CancellationToken cancellationToken)
    {
        var permission = await _repository.FindByIdAsync(request.Id);

        return permission is null
            ? Result<PermissionEntityInfo>.Failure("Permission не найден")
            : Result<PermissionEntityInfo>.Success(permission);
    }
}