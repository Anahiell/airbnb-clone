using Airbnb.PictureManagement.Application.BoundedContext.UserPictureManagement.Commands.ArchiveUserPictureCommand;
using Airbnb.PictureManagement.Application.BoundedContext.UserPictureManagement.Commands.DeleteUserPictureCommand;
using Airbnb.PictureManagement.Application.BoundedContext.UserPictureManagement.Commands.UpdateUserPictureCommand;
using Airbnb.PictureManagement.Application.BoundedContext.UserPictureManagement.Commands.UploadUserPictureCommand;
using Airbnb.PictureManagement.Application.BoundedContext.UserPictureManagement.Queries.GetUserPictureByIdQuery;
using Airbnb.PictureManagement.Application.BoundedContext.UserPictureManagement.Queries.GetUserPicturesPaginatedQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.PictureManagement.API.Controllers;

/// <summary>
/// Контроллер для управления картинками пользователей
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class UserPictureController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Получить список картинок пользователя
    /// </summary>
    [HttpGet("GetAllUserPictures")]
    [SwaggerOperation(Summary = "Получить список картинок пользователя", Description = "С фильтрацией, сортировкой и пагинацией")]
    public async Task<IActionResult> GetAllUserPictures([FromQuery] GetUserPicturesPaginatedQuery query, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result.Value);
    }

    /// <summary>
    /// Получить картинку пользователя по идентификатору
    /// </summary>
    [HttpGet("GetUserPictureByIdAsync")]
    [SwaggerOperation(Summary = "Получить картинку пользователя по ID", Description = "Поиск по идентификатору картинки")]
    public async Task<IActionResult> GetUserPictureByIdAsync([FromQuery] GetUserPictureByIdQuery query, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Загрузить новую картинку пользователя
    /// </summary>
    [HttpPost("UploadUserPictureAsync")]
    [SwaggerOperation(Summary = "Загрузить картинку", Description = "Загружает картинку для пользователя")]
    public async Task<IActionResult> UploadUserPictureAsync([FromForm] UploadUserPictureCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result.Value);
    }

    /// <summary>
    /// Обновить URL картинки пользователя
    /// </summary>
    [HttpPut("UpdateUserPictureAsync")]
    [SwaggerOperation(Summary = "Обновить URL картинки", Description = "Обновляет URL загруженной картинки пользователя")]
    public async Task<IActionResult> UpdateUserPictureAsync([FromQuery] UpdateUserPictureCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Удалить картинку пользователя
    /// </summary>
    [HttpDelete("DeleteUserPictureAsync")]
    [SwaggerOperation(Summary = "Удалить картинку", Description = "Удаляет картинку пользователя по идентификатору")]
    public async Task<IActionResult> DeleteUserPictureAsync([FromQuery] DeleteUserPictureCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Архивировать картинку пользователя
    /// </summary>
    [HttpPost("ArchiveUserPictureAsync")]
    [SwaggerOperation(Summary = "Архивировать картинку", Description = "Архивирует (Soft Delete) картинку пользователя")]
    public async Task<IActionResult> ArchiveUserPictureAsync([FromQuery] ArchiveUserPictureCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }
}