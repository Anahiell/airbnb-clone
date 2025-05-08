using Airbnb.TagsManagement.Application.BoundedContext.ProductTagManagement.Commands.CreateProductTagCommand;
using Airbnb.TagsManagement.Application.BoundedContext.ProductTagManagement.Commands.DeleteProductTagCommand;
using Airbnb.TagsManagement.Application.BoundedContext.ProductTagManagement.Commands.UpdateProductTagCommand;
using Airbnb.TagsManagement.Application.BoundedContext.ProductTagManagement.Queries.GetProductTagsByProductIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.TagManagement.API.Controllers;

/// <summary>
/// Контроллер для взаимодействия с ProductTag
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class ProductTagController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductTagController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    /// Получить список всех тегов для конкретного продукта.
    /// </summary>
    [HttpGet]
    [Route("GetAllProductTags")]
    [SwaggerOperation(Summary = "Получить теги для продукта",
        Description = "Получает теги для продукта из базы данных с пагинацией и фильтрацией")]
    public async Task<IActionResult> GetAllProductTagsAsync([FromQuery] GetProductTagsPaginatedQuery query,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result.Value);
    }

    /// <summary>
    /// Получить теги по Id продукта.
    /// </summary>
    [HttpGet]
    [Route("GetProductTagsById")]
    [SwaggerOperation(Summary = "Получить теги по Id продукта",
        Description = "Получает теги для конкретного продукта по его идентификатору")]
    public async Task<IActionResult> GetProductTagsByIdAsync([FromQuery] GetProductTagsPaginatedQuery query,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result.Value);
    }

    /// <summary>
    /// Создать новый тег для продукта.
    /// </summary>
    [HttpPost]
    [Route("CreateProductTag")]
    [SwaggerOperation(Summary = "Создать тег для продукта", Description = "Создает новый тег для продукта в базе данных")]
    [SwaggerResponse(200, "Успешный запрос", typeof(Guid))]
    [SwaggerResponse(400, "Ошибка валидации", typeof(int))]
    [SwaggerResponse(500, "Ошибка сервера", typeof(int))]
    public async Task<IActionResult> CreateProductTagAsync([FromQuery] CreateProductTagCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result.Value);
    }

    /// <summary>
    /// Обновить тег для продукта.
    /// </summary>
    [HttpPut]
    [Route("UpdateProductTag")]
    [SwaggerOperation(Summary = "Обновить тег для продукта", Description = "Обновляет существующий тег для продукта в базе данных")]
    public async Task<IActionResult> UpdateProductTagAsync([FromQuery] UpdateProductTagCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Удалить тег для продукта.
    /// </summary>
    [HttpDelete]
    [Route("DeleteProductTag")]
    [SwaggerOperation(Summary = "Удалить тег для продукта", Description = "Удаляет тег для продукта по его идентификатору")]
    public async Task<IActionResult> DeleteProductTagAsync([FromQuery] DeleteProductTagCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }
}
