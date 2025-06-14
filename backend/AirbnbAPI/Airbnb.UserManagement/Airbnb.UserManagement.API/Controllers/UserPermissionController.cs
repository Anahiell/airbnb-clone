using Airbnb.UserManagement.Application.BoundedContexts.PermissionManagement.QueryObjects;
using Airbnb.UserManagement.Application.BoundedContexts.UserPermissionManagement.Commands.CreateUserPermissionCommand;
using Airbnb.UserManagement.Application.BoundedContexts.UserPermissionManagement.Commands.DeleteUserPermissionCommand;
using Airbnb.UserManagement.Application.BoundedContexts.UserPermissionManagement.Commands.UpdateUserPermissionCommand;
using Airbnb.UserManagement.Application.BoundedContexts.UserPermissionManagement.Queries.GetAllUserPermissionsQuery;
using Airbnb.UserManagement.Application.BoundedContexts.UserPermissionManagement.Queries.GetUserPermissionByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.UserManagement.API.Controllers;

/// <summary>
/// Контроллер для управления связью Пользователь - Право доступа.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class UserPermissionController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserPermissionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Создать связь Пользователь - Право.
    /// </summary>
    [HttpPost]
    [Route("CreateUserPermission")]
    [SwaggerOperation(Summary = "Создать связь Пользователь - Право", Description = "Назначить право пользователю.")]
    [SwaggerResponse(200, "Связь создана", typeof(int))]
    [SwaggerResponse(400, "Ошибка валидации", typeof(string))]
    public async Task<IActionResult> CreateUserPermissionAsync([FromQuery] CreateUserPermissionCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result.Value);
    }
    
    /// <summary>
    /// Обновить связь Пользователь - Право.
    /// </summary>
    [HttpPut("UpdateUserPermission")]
    [SwaggerOperation(Summary = "Обновить связь Пользователь - Право")]
    public async Task<IActionResult> UpdateUserPermissionAsync([FromQuery] UpdateUserPermissionCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result.Value);
    }

    /// <summary>
    /// Удалить связь Пользователь - Право.
    /// </summary>
    [HttpDelete]
    [Route("DeleteUserPermission")]
    [SwaggerOperation(Summary = "Удалить связь Пользователь - Право", Description = "Удалить право у пользователя.")]
    [SwaggerResponse(200, "Связь удалена", typeof(string))]
    [SwaggerResponse(400, "Ошибка валидации", typeof(string))]
    public async Task<IActionResult> DeleteUserPermissionAsync([FromQuery] DeleteUserPermissionCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result.Value);
    }

    /// <summary>
    /// Получить все право по Id.
    /// </summary>
    [HttpGet]
    [Route("GetUserPermissionsByUserId")]
    [SwaggerOperation(Summary = "Получить права пользователя", Description = "Возвращает список прав, назначенных пользователю.")]
    [SwaggerResponse(200, "Список прав", typeof(IEnumerable<PermissionEntityInfo>))]
    public async Task<IActionResult> GetUserPermissionsByIdAsync([FromQuery] GetUserPermissionByIdQuery query, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result.Value);
    }
    
    /// <summary>
    /// Получить все право по Id.
    /// </summary>
    [HttpGet]
    [Route("GetPermissionsByUserIdAsync")]
    [SwaggerOperation(Summary = "Получить все права пользователя", Description = "Возвращает список прав, назначенных пользователю.")]
    [SwaggerResponse(200, "Список прав", typeof(IEnumerable<PermissionEntityInfo>))]
    public async Task<IActionResult> GetPermissionsByUserIdAsync([FromQuery] GetAllUserPermissionsQuery query, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result.Value);
    }
}