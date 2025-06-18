using Airbnb.Application.Results;
using Airbnb.Application.UseCases;
using Airbnb.Connection.ConnectionService.HttpConnection.Services;
using Airbnb.ProductManagement.Application.BoundedContext.QueryObjects;
using Airbnb.SharedKernel.ConnectionService.HttpConnection;

namespace Airbnb.ProductManagement.Application.BoundedContext.ProductManagement.UseCases.User;

public record GetUserByIdUseCase(int Id) : IUseCase<OwnerEntityInfo>;

public class GetUserByIdUseCaseHandler : IUseCaseHandler<GetUserByIdUseCase, OwnerEntityInfo>
{
    private readonly IHttpConnectionService _connection;

    public GetUserByIdUseCaseHandler(IHttpConnectionService connectionService)
    {
        _connection = connectionService;
    }

    public async Task<OwnerEntityInfo> Handle(GetUserByIdUseCase request, CancellationToken cancellationToken)
    {
        var user = await _connection.GetAsync<OwnerEntityInfo>(
            "/api/v1/User/GetUserById",
            new HttpConnectionData { ClientName = "UserService", CancellationToken = cancellationToken },
            queryParams: new { Id = request.Id });

        if (user == null)
        {
            return null;
        }

        var avatar = new PictureInfo
        {
            Url = user.Avatar.Url,
        };

        var dto = new OwnerEntityInfo
        {
            Name = user.Name,
            Avatar = avatar,
            RegistrationDate = user.RegistrationDate,
            IsVerificated = user.IsVerificated,
            Languages = user.Languages.Select(l => l).ToList(),
            ResponseSpeed = 60,
            ResponseSpeedDuration = "протягом години"
        };

        return dto;
    }
}