using Airbnb.UserManagement.Application.BoundedContexts.UserLangauageManagement.Commands.CreateUserLanguageCommand;
using Airbnb.UserManagement.Application.BoundedContexts.UserLangauageManagement.Commands.DeleteUserLanguageCommand;
using Airbnb.UserManagement.Application.BoundedContexts.UserLangauageManagement.Commands.UpdateUserLanguageCommand;
using Airbnb.UserManagement.Application.BoundedContexts.UserLangauageManagement.Queries;
using Airbnb.UserManagement.Application.BoundedContexts.UserLangauageManagement.QueryObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.UserManagement.API.Controllers;

/// <summary>
/// Контроллер для управления связи языков с пользователя.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class UserLanguageController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserLanguageController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Создать связь языка и пользователя.
    /// </summary>
    [HttpPost]
    [Route("CreateLanguage")]
    [SwaggerOperation(Summary = "Создать язык", Description = "Создает новый язык.")]
    [SwaggerResponse(200, "Успешное создание", typeof(Guid))]
    [SwaggerResponse(400, "Ошибка валидации", typeof(string))]
    public async Task<IActionResult> CreateLanguageAsync([FromQuery] CreateUserLanguageCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result.Value);
    }

    /// <summary>
    /// Обновить связь языка и пользователя.
    /// </summary>
    [HttpPut]
    [Route("UpdateLanguage")]
    [SwaggerOperation(Summary = "Обновить язык", Description = "Обновляет данные существующего языка.")]
    [SwaggerResponse(200, "Успешное обновление", typeof(string))]
    [SwaggerResponse(404, "Язык не найден", typeof(string))]
    public async Task<IActionResult> UpdateLanguageAsync([FromQuery] UpdateUserLanguageCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result.Value);
    }

    /// <summary>
    /// Удалить связь языка и пользователя.
    /// </summary>
    [HttpDelete]
    [Route("DeleteLanguage")]
    [SwaggerOperation(Summary = "Удалить язык", Description = "Удаляет язык по идентификатору.")]
    [SwaggerResponse(200, "Язык удален", typeof(string))]
    [SwaggerResponse(404, "Язык не найден", typeof(string))]
    public async Task<IActionResult> DeleteLanguageAsync([FromQuery] DeleteUserLanguageCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result.Value);
    }

    /// <summary>
    /// Получить список связи языков и пользователей с пагинацией.
    /// </summary>
    [HttpGet]
    [Route("GetUserLanguages")]
    [SwaggerOperation(Summary = "Получить список языков пользователя", Description = "Возвращает список языков пользователя с возможностью пагинации и фильтрации.")]
    [SwaggerResponse(200, "Успешный результат", typeof(IEnumerable<UserLanguageEntityInfo>))]
    [SwaggerResponse(400, "Ошибка валидации", typeof(string))]
    public async Task<IActionResult> GetUserLanguagesAsync([FromQuery] GetUserLanguagePaginatedQuery query, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result.Value);
    }
}