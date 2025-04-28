using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.UserManagement.API.Controllers;

/// <summary>
/// Контроллер для регистрации и логина
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Регистрация пользователя.
    /// </summary>
    [HttpPost]
    [Route("Register")]
    [SwaggerOperation(Summary = "Регистрация пользователя", Description = "Регистрирует нового пользователя.")]
    [SwaggerResponse(200, "Успешная регистрация", typeof(string))]
    [SwaggerResponse(400, "Ошибка валидации", typeof(string))]
    [SwaggerResponse(500, "Ошибка сервера", typeof(string))]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result.Value);
    }

    /// <summary>
    /// Логин пользователя.
    /// </summary>
    [HttpPost]
    [Route("Login")]
    [SwaggerOperation(Summary = "Логин пользователя", Description = "Позволяет пользователю войти в систему.")]
    [SwaggerResponse(200, "Успешный логин", typeof(string))]
    [SwaggerResponse(400, "Ошибка валидации", typeof(string))]
    [SwaggerResponse(500, "Ошибка сервера", typeof(string))]
    public async Task<IActionResult> LoginAsync([FromBody] LoginCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result.Value);
    }
}