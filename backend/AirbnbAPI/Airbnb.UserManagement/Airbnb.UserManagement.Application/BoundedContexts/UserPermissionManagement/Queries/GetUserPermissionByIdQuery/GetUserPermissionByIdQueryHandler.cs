using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.MongoRepository.Repositories;
using Airbnb.UserManagement.Application.BoundedContexts.UserPermissionManagement.QueryObjects;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserPermissionManagement.Queries.GetUserPermissionByIdQuery;

public class GetUserPermissionByIdQueryHandler : IQueryHandler<GetUserPermissionByIdQuery, Result<UserPermissionEntityInfo>>
{
    private readonly BaseMongoRepository<UserPermissionEntityInfo> _repository;

    public GetUserPermissionByIdQueryHandler(BaseMongoRepository<UserPermissionEntityInfo> repository)
    {
        _repository = repository;
    }

    public async Task<Result<UserPermissionEntityInfo>> Handle(GetUserPermissionByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.FindByIdAsync(request.Id);
        return entity is null
            ? Result<UserPermissionEntityInfo>.Failure("Связь пользователь-права не найдена")
            : Result<UserPermissionEntityInfo>.Success(entity);
    }
}