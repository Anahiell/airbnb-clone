using Airbnb.PictureManagement.Application.BoundedContext.Commands;
using Airbnb.PictureManagement.Application.BoundedContext.Commands.DeletePictureCommand;
using Airbnb.PictureManagement.Application.BoundedContext.Commands.UpdatePictureCommand;
using Airbnb.PictureManagement.Application.BoundedContext.ProductPictureManagement.Commands.ArchiveProductPictureCommand;
using Airbnb.PictureManagement.Application.BoundedContext.ProductPictureManagement.Queries.GetProductPictureByIdQuery;
using Airbnb.PictureManagement.Application.BoundedContext.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.PictureManagement.API.Controllers;

/// <summary>
/// Контроллер для управления картинками продуктов
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class ProductPictureController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Получить список картинок продукта
    /// </summary>
    [HttpGet("GetAllProductPictures")]
    [SwaggerOperation(Summary = "Получить список картинок продукта", Description = "С фильтрацией, сортировкой и пагинацией")]
    public async Task<IActionResult> GetAllProductPictures([FromQuery] GetProductPicturesPaginatedQuery query, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result.Value);
    }

    /// <summary>
    /// Получить картинку продукта по идентификатору
    /// </summary>
    [HttpGet("GetProductPictureByIdAsync")]
    [SwaggerOperation(Summary = "Получить картинку по ID", Description = "Получить картинку продукта по идентификатору")]
    public async Task<IActionResult> GetProductPictureByIdAsync([FromQuery] GetProductPictureByIdQuery query, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result.Value);
    }

    /// <summary>
    /// Загрузить новую картинку продукта
    /// </summary>
    [HttpPost("UploadProductPictureAsync")]
    [SwaggerOperation(Summary = "Загрузить картинку", Description = "Загружает новую картинку для продукта")]
    [SwaggerResponse(200, "Картинка загружена", typeof(string))]
    public async Task<IActionResult> UploadProductPictureAsync([FromForm] UploadProductPictureCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result.Value);
    }

    /// <summary>
    /// Обновить URL картинки продукта
    /// </summary>
    [HttpPut("UpdateProductPictureAsync")]
    [SwaggerOperation(Summary = "Обновить картинку", Description = "Обновляет URL существующей картинки продукта")]
    public async Task<IActionResult> UpdateProductPictureAsync([FromQuery] UpdateProductImageCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Удалить картинку продукта
    /// </summary>
    [HttpDelete("DeleteProductPictureAsync")]
    [SwaggerOperation(Summary = "Удалить картинку", Description = "Удаляет картинку продукта из базы")]
    public async Task<IActionResult> DeleteProductPictureAsync([FromQuery] DeleteProductImageCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Архивировать картинку продукта
    /// </summary>
    [HttpPost("ArchiveProductPictureAsync")]
    [SwaggerOperation(Summary = "Архивировать картинку", Description = "Архивирует картинку продукта (soft delete)")]
    public async Task<IActionResult> ArchiveProductPictureAsync([FromQuery] ArchiveProductPictureCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }
}