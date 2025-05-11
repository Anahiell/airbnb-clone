using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.Connection.ConnectionService.HttpConnection.Services;
using Airbnb.MongoRepository.Repositories;
using Airbnb.SharedKernel.ConnectionService.HttpConnection;
using Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.QueryObjects;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Queries.GetUserByIdQuery;

public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, Result<UserEntityInfo>>
{
    private readonly BaseMongoRepository<UserEntityInfo> _repository;
    private readonly IHttpConnectionService  _connection;

    public GetUserByIdQueryHandler(BaseMongoRepository<UserEntityInfo> repository, IHttpConnectionService connection)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _connection = connection;
    }

    public async Task<Result<UserEntityInfo>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = (await _repository.FindByAsync(o => o.Id == request.Id)).FirstOrDefault();

        if (user == null)
        {
            // return Result<UserEntityInfo>.Failure($"Пользователь с Id {request.Id} не найден.");
        }
        Console.WriteLine("UserId:" + user.Id);
        var picture = await _connection.GetAsync<PictureInfo>(
            "api/v1/UserPicture/GetUserPictureByIdAsync",
            new HttpConnectionData { ClientName = "PictureService", CancellationToken = cancellationToken },
            new { UserId = user.Id });
        user.Url = picture;
        Console.WriteLine("Picture:" + picture);
        return Result<UserEntityInfo>.Success(user);
    }
}