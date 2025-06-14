using Airbnb.UserManagement.Application.BoundedContexts.LanguageManagement.Commands.CreateLanguageCommand;
using Airbnb.UserManagement.Application.BoundedContexts.LanguageManagement.Commands.DeleteLanguageCommand;
using Airbnb.UserManagement.Application.BoundedContexts.LanguageManagement.Commands.UpdateLanguageCommand;
using Airbnb.UserManagement.Application.BoundedContexts.LanguageManagement.Queries;
using Airbnb.UserManagement.Application.BoundedContexts.UserLangauageManagement.QueryObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.UserManagement.API.Controllers;

/// <summary>
/// Контроллер для управления языками.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class LanguageController : ControllerBase
{
    private readonly IMediator _mediator;

    public LanguageController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Создать язык.
    /// </summary>
    [HttpPost]
    [Route("CreateLanguage")]
    [SwaggerOperation(Summary = "Создать язык", Description = "Создает новый язык.")]
    [SwaggerResponse(200, "Успешное создание", typeof(int))]
    [SwaggerResponse(400, "Ошибка валидации", typeof(string))]
    public async Task<IActionResult> CreateLanguageAsync([FromQuery] CreateLanguageCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result.Value);
    }

    /// <summary>
    /// Обновить язык.
    /// </summary>
    [HttpPut]
    [Route("UpdateLanguage")]
    [SwaggerOperation(Summary = "Обновить язык", Description = "Обновляет данные существующего языка.")]
    [SwaggerResponse(200, "Успешное обновление", typeof(string))]
    [SwaggerResponse(404, "Язык не найден", typeof(string))]
    public async Task<IActionResult> UpdateLanguageAsync([FromQuery] UpdateLanguageCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result.Value);
    }

    /// <summary>
    /// Удалить язык.
    /// </summary>
    [HttpDelete]
    [Route("DeleteLanguage")]
    [SwaggerOperation(Summary = "Удалить язык", Description = "Удаляет язык по идентификатору.")]
    [SwaggerResponse(200, "Язык удален", typeof(string))]
    [SwaggerResponse(404, "Язык не найден", typeof(string))]
    public async Task<IActionResult> DeleteLanguageAsync([FromQuery] DeleteLanguageCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result.Value);
    }

    /// <summary>
    /// Получить список языков с пагинацией.
    /// </summary>
    [HttpGet]
    [Route("GetLanguages")]
    [SwaggerOperation(Summary = "Получить список языков", Description = "Возвращает список языков с возможностью пагинации и фильтрации.")]
    [SwaggerResponse(200, "Успешный результат", typeof(IEnumerable<LanguageEntityInfo>))]
    [SwaggerResponse(400, "Ошибка валидации", typeof(string))]
    public async Task<IActionResult> GetLanguagesAsync([FromQuery] GetAllLanguagesQuery query, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result.Value);
    }
    
    /// <summary>
    /// Получить язык по Id.
    /// </summary>
    [HttpGet]
    [Route("GetLanguageById")]
    [SwaggerOperation(Summary = "Получить язык по Id", Description = "Возвращает язык по Id.")]
    [SwaggerResponse(200, "Успешный результат", typeof(IEnumerable<LanguageEntityInfo>))]
    [SwaggerResponse(400, "Ошибка валидации", typeof(string))]
    public async Task<IActionResult> GetLanguagesAsync([FromQuery] GetLanguageByIdQuery query, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result.Value);
    }
}