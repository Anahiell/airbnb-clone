using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.MongoRepository.Repositories;
using Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.QueryObjects;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Queries.GetUserByIdQuery;

public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, Result<UserEntityInfo>>
{
    private readonly BaseMongoRepository<UserEntityInfo> _repository;

    public GetUserByIdQueryHandler(BaseMongoRepository<UserEntityInfo> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Result<UserEntityInfo>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = (await _repository.FindByAsync(o => o.Id == request.Id)).FirstOrDefault();

        if (user == null)
        {
            // return Result<UserEntityInfo>.Failure($"Пользователь с Id {request.Id} не найден.");
        }

        return Result<UserEntityInfo>.Success(user);
    }
}