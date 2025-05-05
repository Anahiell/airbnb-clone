using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.PictureManagement.Application.BoundedContext.UserPictureManagement.QueryObjects;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.PictureManagement.Application.BoundedContext.UserPictureManagement.Queries.GetUserPictureByIdQuery;

/// <summary>
/// Запрос на получение картинки пользователя по его UserId
/// </summary>
[SwaggerSchema("Запрос на получение картинки пользователя по его UserId")]
public class GetUserPictureByIdQuery : IQuery<Result<UserPictureEntityInfo>>
{
    [SwaggerSchema("ID пользователя")]
    public int UserId { get; set; }
}