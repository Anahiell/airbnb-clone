using Airbnb.UserManagement.Application.BoundedContexts.PermissionManagement.Commands.CreatePermissionCommand;
using Airbnb.UserManagement.Application.BoundedContexts.PermissionManagement.Commands.DeletePermissionCommand;
using Airbnb.UserManagement.Application.BoundedContexts.PermissionManagement.Commands.UpdatePermissionCommand;
using Airbnb.UserManagement.Application.BoundedContexts.PermissionManagement.Queries;
using Airbnb.UserManagement.Application.BoundedContexts.PermissionManagement.Queries.GetAllPermissionsQuery;
using Airbnb.UserManagement.Application.BoundedContexts.PermissionManagement.QueryObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.UserManagement.API.Controllers;

/// <summary>
/// Контроллер для управления правами доступа.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class PermissionController : ControllerBase
{
    private readonly IMediator _mediator;

    public PermissionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Создать новое право.
    /// </summary>
    [HttpPost]
    [Route("CreatePermission")]
    [SwaggerOperation(Summary = "Создать новое право", Description = "Создаёт новое право доступа.")]
    [SwaggerResponse(200, "Успешное создание", typeof(int))]
    [SwaggerResponse(400, "Ошибка валидации", typeof(string))]
    public async Task<IActionResult> CreatePermissionAsync([FromQuery] CreatePermissionCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result.Value);
    }

    /// <summary>
    /// Обновить право доступа.
    /// </summary>
    [HttpPut]
    [Route("UpdatePermission")]
    [SwaggerOperation(Summary = "Обновить право доступа", Description = "Обновляет существующее право.")]
    [SwaggerResponse(200, "Право обновлено", typeof(string))]
    [SwaggerResponse(400, "Ошибка валидации", typeof(string))]
    [SwaggerResponse(404, "Право не найдено", typeof(string))]
    public async Task<IActionResult> UpdatePermissionAsync([FromQuery] UpdatePermissionCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result.Value);
    }

    /// <summary>
    /// Удалить право доступа.
    /// </summary>
    [HttpDelete]
    [Route("DeletePermission")]
    [SwaggerOperation(Summary = "Удалить право доступа", Description = "Удаляет право по его ID.")]
    [SwaggerResponse(200, "Право удалено", typeof(string))]
    [SwaggerResponse(400, "Ошибка валидации", typeof(string))]
    [SwaggerResponse(404, "Право не найдено", typeof(string))]
    public async Task<IActionResult> DeletePermissionAsync([FromQuery] DeletePermissionCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result.Value);
    }

    /// <summary>
    /// Получить все права доступа.
    /// </summary>
    [HttpGet]
    [Route("GetAllPermissions")]
    [SwaggerOperation(Summary = "Получить все права", Description = "Возвращает список всех прав доступа.")]
    [SwaggerResponse(200, "Список прав", typeof(IEnumerable<PermissionEntityInfo>))]
    public async Task<IActionResult> GetAllPermissionsAsync([FromQuery] GetAllPermissionsQuery query, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result.Value);
    }

    /// <summary>
    /// Получить право по Id.
    /// </summary>
    [HttpGet]
    [Route("GetPermissionById")]
    [SwaggerOperation(Summary = "Получить право по ID", Description = "Возвращает право по его идентификатору.")]
    [SwaggerResponse(200, "Право найдено", typeof(PermissionEntityInfo))]
    [SwaggerResponse(404, "Право не найдено", typeof(string))]
    public async Task<IActionResult> GetPermissionByIdAsync([FromQuery] GetPermissionByIdQuery query, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result.Value);
    }
}