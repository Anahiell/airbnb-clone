using Airbnb.ReviewManagement.Application.BoundedContext.Commands;
using Airbnb.ReviewManagement.Application.BoundedContext.Commands.DeleteReviewCommand;
using Airbnb.ReviewManagement.Application.BoundedContext.Commands.UpdateReviewCommand;
using Airbnb.ReviewManagement.Application.BoundedContext.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.ReviewManagement.API.Controllers;

/// <summary>
/// Контроллер для взаимодействия с отзывами
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class ReviewController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Получить список отзывов.
    /// </summary>
    [HttpGet]
    [Route("GetAllReviews")]
    [SwaggerOperation(Summary = "Получить отзывы",
        Description = "Получает список отзывов с использованием пагинации и фильтрации.")]
    public async Task<IActionResult> GetAllReviewsAsync([FromQuery] GetReviewPaginatedQuery query,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result.Value);
    }

    /// <summary>
    /// Получить отзыв по Id.
    /// </summary>
    [HttpGet]
    [Route("GetReviewById")]
    [SwaggerOperation(Summary = "Получить отзыв по Id",
        Description = "Получает конкретный отзыв по его идентификатору.")]
    public async Task<IActionResult> GetReviewByIdAsync([FromQuery] GetReviewPaginatedQuery query,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Создать отзыв.
    /// </summary>
    [HttpPost]
    [Route("CreateReview")]
    [SwaggerOperation(Summary = "Создать отзыв", Description = "Создает новый отзыв.")]
    [SwaggerResponse(200, "Успешное создание", typeof(Guid))]
    [SwaggerResponse(400, "Ошибка валидации", typeof(string))]
    [SwaggerResponse(500, "Ошибка сервера", typeof(string))]
    public async Task<IActionResult> CreateReviewAsync([FromBody] CreateReviewCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result.Value);
    }

    /// <summary>
    /// Обновить отзыв.
    /// </summary>
    [HttpPut]
    [Route("UpdateReview")]
    [SwaggerOperation(Summary = "Обновить отзыв", Description = "Обновляет существующий отзыв.")]
    public async Task<IActionResult> UpdateReviewAsync([FromBody] UpdateReviewCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Удалить отзыв.
    /// </summary>
    [HttpDelete]
    [Route("DeleteReview")]
    [SwaggerOperation(Summary = "Удалить отзыв", Description = "Удаляет отзыв по идентификатору.")]
    public async Task<IActionResult> DeleteReviewAsync([FromBody] DeleteReviewCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }
}