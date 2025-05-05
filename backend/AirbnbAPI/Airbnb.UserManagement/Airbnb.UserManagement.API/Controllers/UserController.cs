using Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Commands.UpdateUserCommand;
using Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Commands.UserCreateCommand;
using Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Queries.GetUserByIdQuery;
using Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.QueryObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.UserManagement.API.Controllers;

/// <summary>
/// Контроллер для взаимодействия с пользователями
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Создать пользователя.
    /// </summary>
    [HttpPost]
    [Route("CreateUser")]
    [SwaggerOperation(Summary = "Создать пользователя", Description = "Создает нового пользователя.")]
    [SwaggerResponse(200, "Успешное создание", typeof(int))]
    [SwaggerResponse(400, "Ошибка валидации", typeof(string))]
    [SwaggerResponse(500, "Ошибка сервера", typeof(string))]
    public async Task<IActionResult> CreateUserAsync([FromQuery] CreateUserCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result.Value);
    }

    /// <summary>
    /// Обновить пользователя.
    /// </summary>
    [HttpPut]
    [Route("UpdateUser")]
    [SwaggerOperation(Summary = "Обновить пользователя", Description = "Обновляет существующего пользователя.")]
    public async Task<IActionResult> UpdateUserAsync([FromQuery] UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }
    
    /// <summary>
    /// Получить пользователя по Id.
    /// </summary>
    [HttpGet]
    [Route("GetUserById")]
    [SwaggerOperation(Summary = "Получить пользователя по Id", Description = "Возвращает пользователя по его идентификатору.")]
    [SwaggerResponse(200, "Успешный результат", typeof(UserEntityInfo))]
    [SwaggerResponse(404, "Пользователь не найден", typeof(string))]
    public async Task<IActionResult> GetUserByIdAsync([FromQuery] GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);

        return Ok(result.Value);
    }
}
