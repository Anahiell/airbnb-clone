using Airbnb.UserManagement.Application.BoundedContexts.RoleManagement.Commands.CreateRoleCommand;
using Airbnb.UserManagement.Application.BoundedContexts.RoleManagement.Commands.DeleteRoleCommand;
using Airbnb.UserManagement.Application.BoundedContexts.RoleManagement.Commands.UpdateRoleCommand;
using Airbnb.UserManagement.Application.BoundedContexts.RoleManagement.Queries.GetAllRolesQuery;
using Airbnb.UserManagement.Application.BoundedContexts.RoleManagement.Queries.GetRoleByIdQuery;
using Airbnb.UserManagement.Application.BoundedContexts.RoleManagement.QueryObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.UserManagement.API.Controllers;

/// <summary>
/// Контроллер для управления ролями.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class RoleController : ControllerBase
{
    private readonly IMediator _mediator;

    public RoleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Создать новую роль.
    /// </summary>
    [HttpPost]
    [Route("CreateRole")]
    [SwaggerOperation(Summary = "Создать новую роль", Description = "Создаёт новую роль.")]
    [SwaggerResponse(200, "Успешное создание", typeof(int))]
    [SwaggerResponse(400, "Ошибка валидации", typeof(string))]
    public async Task<IActionResult> CreateRoleAsync([FromQuery] CreateRoleCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result.Value);
    }

    /// <summary>
    /// Обновить роль.
    /// </summary>
    [HttpPut]
    [Route("UpdateRole")]
    [SwaggerOperation(Summary = "Обновить роль", Description = "Обновляет существующую роль.")]
    [SwaggerResponse(200, "Роль обновлена", typeof(string))]
    [SwaggerResponse(400, "Ошибка валидации", typeof(string))]
    [SwaggerResponse(404, "Роль не найдена", typeof(string))]
    public async Task<IActionResult> UpdateRoleAsync([FromQuery] UpdateRoleCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result.Value);
    }

    /// <summary>
    /// Удалить роль.
    /// </summary>
    [HttpDelete]
    [Route("DeleteRole")]
    [SwaggerOperation(Summary = "Удалить роль", Description = "Удаляет роль по её ID.")]
    [SwaggerResponse(200, "Роль удалена", typeof(string))]
    [SwaggerResponse(400, "Ошибка валидации", typeof(string))]
    [SwaggerResponse(404, "Роль не найдена", typeof(string))]
    public async Task<IActionResult> DeleteRoleAsync([FromQuery] DeleteRoleCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result.Value);
    }

    /// <summary>
    /// Получить все роли.
    /// </summary>
    [HttpGet]
    [Route("GetAllRoles")]
    [SwaggerOperation(Summary = "Получить все роли", Description = "Возвращает список всех ролей.")]
    [SwaggerResponse(200, "Список ролей", typeof(IEnumerable<RoleEntityInfo>))]
    public async Task<IActionResult> GetAllRolesAsync([FromQuery] GetAllRolesQuery query, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result.Value);
    }

    /// <summary>
    /// Получить роль по Id.
    /// </summary>
    [HttpGet]
    [Route("GetRoleById")]
    [SwaggerOperation(Summary = "Получить роль по ID", Description = "Возвращает роль по её идентификатору.")]
    [SwaggerResponse(200, "Роль найдена", typeof(RoleEntityInfo))]
    [SwaggerResponse(404, "Роль не найдена", typeof(string))]
    public async Task<IActionResult> GetRoleByIdAsync([FromQuery] GetRoleByIdQuery query, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result.Value);
    }
}