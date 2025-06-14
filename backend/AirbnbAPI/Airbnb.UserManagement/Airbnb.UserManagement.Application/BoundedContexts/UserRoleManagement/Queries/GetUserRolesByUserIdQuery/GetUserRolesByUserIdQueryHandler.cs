using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.MongoRepository.Repositories;
using Airbnb.UserManagement.Application.BoundedContexts.UserRoleManagement.QueryObjects;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserRoleManagement.Queries.GetUserRolesByUserIdQuery;

public class GetUserRolesByUserIdQueryHandler : IQueryHandler<GetUserRolesByUserIdQuery, Result<IEnumerable<UserRoleEntityInfo>>>
{
    private readonly BaseMongoRepository<UserRoleEntityInfo> _repository;

    public GetUserRolesByUserIdQueryHandler(BaseMongoRepository<UserRoleEntityInfo> repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<UserRoleEntityInfo>>> Handle(GetUserRolesByUserIdQuery request, CancellationToken cancellationToken)
    {
        var items = await _repository.FindByAsync(x => x.UserId == request.UserId);
        return Result<IEnumerable<UserRoleEntityInfo>>.Success(items);
    }
}