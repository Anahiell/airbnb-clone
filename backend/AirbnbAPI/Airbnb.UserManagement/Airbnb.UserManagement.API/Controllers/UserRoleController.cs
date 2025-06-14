using Airbnb.UserManagement.Application.BoundedContexts.RoleManagement.QueryObjects;
using Airbnb.UserManagement.Application.BoundedContexts.UserRoleManagement.Commands.CreateUserRoleCommand;
using Airbnb.UserManagement.Application.BoundedContexts.UserRoleManagement.Commands.DeleteUserRoleCommand;
using Airbnb.UserManagement.Application.BoundedContexts.UserRoleManagement.Commands.UpdateUserRoleCommand;
using Airbnb.UserManagement.Application.BoundedContexts.UserRoleManagement.Queries.GetUserRolesByUserIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.UserManagement.API.Controllers;

/// <summary>
/// Контроллер для управления связью Пользователь - Роль.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class UserRoleController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserRoleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Создать связь Пользователь - Роль.
    /// </summary>
    [HttpPost]
    [Route("CreateUserRole")]
    [SwaggerOperation(Summary = "Создать связь Пользователь - Роль", Description = "Назначить роль пользователю.")]
    [SwaggerResponse(200, "Связь создана", typeof(int))]
    [SwaggerResponse(400, "Ошибка валидации", typeof(string))]
    public async Task<IActionResult> CreateUserRoleAsync([FromQuery] CreateUserRoleCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result.Value);
    }
    
    /// <summary>
    /// Обновить связь Пользователь - Роль.
    /// </summary>
    [HttpPut("UpdateUserRole")]
    [SwaggerOperation(Summary = "Обновить связь Пользователь - Роль")]
    public async Task<IActionResult> UpdateUserRoleAsync([FromQuery] UpdateUserRoleCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result.Value);
    }

    /// <summary>
    /// Удалить связь Пользователь - Роль.
    /// </summary>
    [HttpDelete]
    [Route("DeleteUserRole")]
    [SwaggerOperation(Summary = "Удалить связь Пользователь - Роль", Description = "Удалить роль у пользователя.")]
    [SwaggerResponse(200, "Связь удалена", typeof(string))]
    [SwaggerResponse(400, "Ошибка валидации", typeof(string))]
    public async Task<IActionResult> DeleteUserRoleAsync([FromQuery] DeleteUserRoleCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result.Value);
    }

    /// <summary>
    /// Получить все роли пользователя по Id.
    /// </summary>
    [HttpGet]
    [Route("GetUserRolesByUserId")]
    [SwaggerOperation(Summary = "Получить роли пользователя", Description = "Возвращает список ролей, назначенных пользователю.")]
    [SwaggerResponse(200, "Список ролей", typeof(IEnumerable<RoleEntityInfo>))]
    public async Task<IActionResult> GetUserRolesByUserIdAsync([FromQuery] GetUserRolesByUserIdQuery query, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result.Value);
    }
}