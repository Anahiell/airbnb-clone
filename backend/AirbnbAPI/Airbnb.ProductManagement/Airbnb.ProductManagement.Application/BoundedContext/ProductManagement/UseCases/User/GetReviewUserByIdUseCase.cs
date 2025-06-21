using Airbnb.Application.UseCases;
using Airbnb.Connection.ConnectionService.HttpConnection.Services;
using Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;
using Airbnb.SharedKernel.ConnectionService.HttpConnection;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductManagement.UseCases.User;

public record GetReviewUserByIdUseCase(int Id) : IUseCase<ReviewUserEntityInfo>;

public class GetReviewUserByIdUseCaseHandler : IUseCaseHandler<GetReviewUserByIdUseCase, ReviewUserEntityInfo>
{
    private readonly IHttpConnectionService _connection;

    public GetReviewUserByIdUseCaseHandler(IHttpConnectionService connection)
    {
        _connection = connection;
    }

    public async Task<ReviewUserEntityInfo> Handle(GetReviewUserByIdUseCase request, CancellationToken cancellationToken)
    {
        var user = await _connection.GetAsync<OwnerEntityInfo>(
            "/api/v1/User/GetUserById",
                                     new HttpConnectionData
            {
                ClientName = "UserService",
                CancellationToken = cancellationToken
            },
            queryParams: new { Id = request.Id });

        if (user == null)
        {
            return null;
        }

        return new ReviewUserEntityInfo
        {
            UserAvatar = user.Url!.Url,
            Alt = user.FullName,
            UserName = user.FullName
        };
    }
}

public class ReviewUserEntityInfo
{
    public string UserAvatar { get; set; }
    public string Alt { get; set; }
    public string UserName { get; set; }
}