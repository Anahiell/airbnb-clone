using Airbnb.ProductManagement.Application.BoundedContext.ProductFacilityManagement.Queries.GetFacilityPaginatedQuery;
using Airbnb.ProductManagement.Application.BoundedContext.ProductFeatureManagement.Commands.CreateFeatureCommand;
using Airbnb.ProductManagement.Application.BoundedContext.ProductFeatureManagement.Commands.DeleteFeatureCommand;
using Airbnb.ProductManagement.Application.BoundedContext.ProductFeatureManagement.Commands.UpdateFeatureCommand;
using Airbnb.ProductManagement.Application.BoundedContext.ProductFeatureManagement.Queries.GetFeaturePaginatedQuery;
using Airbnb.ProductManagement.Application.BoundedContext.ProductFeatureManagement.QueryObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AirbnbAPI.Controllers;

/// <summary>
/// Контроллер для взаимодействия с фичами (Feature)
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class FeatureController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Создать новую фичу (Feature).
    /// </summary>
    [HttpPost]
    [Route("CreateFeatureAsync")]
    [SwaggerOperation(Summary = "Создать Feature", Description = "Создает новую фичу")]
    [SwaggerResponse(200, "Успешно создано", typeof(int))]
    [SwaggerResponse(400, "Ошибка валидации")]
    public async Task<IActionResult> CreateFeatureAsync([FromQuery] CreateFeatureCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result.Value);
    }

    /// <summary>
    /// Обновить существующую фичу (Feature).
    /// </summary>
    [HttpPut]
    [Route("UpdateFeatureAsync")]
    [SwaggerOperation(Summary = "Обновить Feature", Description = "Обновляет фичу")]
    [SwaggerResponse(200, "Успешно обновлено")]
    [SwaggerResponse(400, "Ошибка валидации")]
    public async Task<IActionResult> UpdateFeatureAsync([FromQuery] UpdateFeatureCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Удалить фичу (Feature).
    /// </summary>
    [HttpDelete]
    [Route("DeleteFeatureAsync")]
    [SwaggerOperation(Summary = "Удалить Feature", Description = "Удаляет фичу по идентификатору")]
    [SwaggerResponse(200, "Успешно удалено")]
    [SwaggerResponse(404, "Feature не найден")]
    public async Task<IActionResult> DeleteFeatureAsync([FromQuery] DeleteFeatureCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Получить список всех фич (Feature).
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// GET http://localhost:8080/api/v1/Feature/GetAll
    /// </remarks>
    [HttpGet]
    [Route("GetAll")]
    [SwaggerOperation(Summary = "Получить все Feature", Description = "Получает все фичи из базы данных")]
    [SwaggerResponse(200, "Список Feature", typeof(List<FeatureEntityInfo>))]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var query = new GetAllFeaturesQuery();
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result.Value);
    }

    /// <summary>
    /// Получить фичу по Id.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// GET http://localhost:8080/api/v1/Feature/GetById?featureId=1
    /// </remarks>
    [HttpGet]
    [Route("GetById")]
    [SwaggerOperation(Summary = "Получить Feature по Id", Description = "Получает фичу по ее идентификатору")]
    [SwaggerResponse(200, "Фича найдена", typeof(FeatureEntityInfo))]
    [SwaggerResponse(404, "Фича не найдена")]
    public async Task<IActionResult> GetByIdAsync([FromQuery] GetFeatureByIdQuery query, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);

        if (result.Value == null)
            return NotFound(result.Errors);

        return Ok(result.Value);
    }
}