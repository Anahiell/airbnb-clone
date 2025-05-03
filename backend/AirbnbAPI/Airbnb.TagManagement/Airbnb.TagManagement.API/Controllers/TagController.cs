using Airbnb.TagsManagement.Application.BoundedContext.Commands.CreateTag;
using Airbnb.TagsManagement.Application.BoundedContext.Commands.DeleteTag;
using Airbnb.TagsManagement.Application.BoundedContext.Commands.UpdateTag;
using Airbnb.TagsManagement.Application.BoundedContext.Queries.GetTagPaginatedQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.TagManagement.API.Controllers;

/// <summary>
/// Контроллер для взаимодействия с Тегами
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class TagController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Получить список всех тегов.
    /// </summary>
    [HttpGet]
    [Route("GetAllTags")]
    [SwaggerOperation(Summary = "Получить теги",
        Description = "Получает теги из базы данных с пагинацией и фильтрацией")]
    public async Task<IActionResult> GetAllTagsAsync([FromQuery] GetTagPaginatedQuery query,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result.Value);
    }

    /// <summary>
    /// Получить тег по Id.
    /// </summary>
    [HttpGet]
    [Route("GetTagById")]
    [SwaggerOperation(Summary = "Получить тег по Id",
        Description = "Получает тег из базы данных по его идентификатору")]
    public async Task<IActionResult> GetTagByIdAsync([FromQuery] GetTagPaginatedQuery query,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result.Value);
    }

    /// <summary>
    /// Создать новый тег.
    /// </summary>
    [HttpPost]
    [Route("CreateTag")]
    [SwaggerOperation(Summary = "Создать тег", Description = "Создает новый тег в базе данных")]
    [SwaggerResponse(200, "Успешный запрос", typeof(Guid))]
    [SwaggerResponse(400, "Ошибка валидации", typeof(int))]
    [SwaggerResponse(500, "Ошибка сервера", typeof(int))]
    public async Task<IActionResult> CreateTagAsync([FromBody] CreateTagCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result.Value);
    }

    /// <summary>
    /// Обновить тег.
    /// </summary>
    [HttpPut]
    [Route("UpdateTag")]
    [SwaggerOperation(Summary = "Обновить тег", Description = "Обновляет существующий тег в базе данных")]
    public async Task<IActionResult> UpdateTagAsync([FromBody] UpdateTagCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Удалить тег.
    /// </summary>
    [HttpDelete]
    [Route("DeleteTag")]
    [SwaggerOperation(Summary = "Удалить тег", Description = "Удаляет тег из базы данных по идентификатору")]
    public async Task<IActionResult> DeleteTagAsync([FromBody] DeleteTagCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }
}