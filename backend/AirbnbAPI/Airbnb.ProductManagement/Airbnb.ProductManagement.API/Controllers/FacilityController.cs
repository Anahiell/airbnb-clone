using System.Text.Json;
using Airbnb.ProductManagement.Application.BoundedContext.ProductFacilityManagement.Commands.CreateFacilityCommand;
using Airbnb.ProductManagement.Application.BoundedContext.ProductFacilityManagement.Commands.DeleteFacilityCommand;
using Airbnb.ProductManagement.Application.BoundedContext.ProductFacilityManagement.Commands.UpdateFacilityCommand;
using Airbnb.ProductManagement.Application.BoundedContext.ProductFacilityManagement.Queries.GetFacilityByIdQuery;
using Airbnb.ProductManagement.Application.BoundedContext.ProductFacilityManagement.Queries.GetFacilityPaginatedQuery;
using Airbnb.ProductManagement.Application.BoundedContext.ProductFacilityManagement.QueryObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AirbnbAPI.Controllers;

/// <summary>
/// Контроллер для взаимодействия с удобствами (Facility)
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class FacilityController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Создать новое удобство (Facility).
    /// </summary>
    [HttpPost]
    [Route("CreateFacilityAsync")]
    [SwaggerOperation(Summary = "Создать Facility", Description = "Создает новое удобство")]
    [SwaggerResponse(200, "Успешно создано", typeof(int))]
    [SwaggerResponse(400, "Ошибка валидации")]
    public async Task<IActionResult> CreateFacilityAsync([FromQuery] CreateFacilityCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result.Value);
    }

    /// <summary>
    /// Обновить существующее удобство (Facility).
    /// </summary>
    [HttpPut]
    [Route("UpdateFacilityAsync")]
    [SwaggerOperation(Summary = "Обновить Facility", Description = "Обновляет удобство")]
    [SwaggerResponse(200, "Успешно обновлено")]
    [SwaggerResponse(400, "Ошибка валидации")]
    public async Task<IActionResult> UpdateFacilityAsync([FromQuery] UpdateFacilityCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Удалить удобство (Facility).
    /// </summary>
    [HttpDelete]
    [Route("DeleteFacilityAsync")]
    [SwaggerOperation(Summary = "Удалить Facility", Description = "Удаляет удобство по идентификатору")]
    [SwaggerResponse(200, "Успешно удалено")]
    [SwaggerResponse(404, "Facility не найден")]
    public async Task<IActionResult> DeleteFacilityAsync([FromQuery] DeleteFacilityCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }
    
    /// <summary>
    /// Получить список всех удобств (Facility).
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// GET http://localhost:8080/api/v1/Facility/GetAll
    /// </remarks>
    [HttpGet]
    [Route("GetAll")]
    [SwaggerOperation(Summary = "Получить все Facility", Description = "Получает все удобства из базы данных")]
    [SwaggerResponse(200, "Список Facility", typeof(List<FacilityEntityInfo>))]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var query = new GetAllFacilitiesQuery();
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result.Value);
    }

    /// <summary>
    /// Получить удобство по Id.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// GET http://localhost:8080/api/v1/Facility/GetById?facilityId=1
    /// </remarks>
    [HttpGet]
    [Route("GetById")]
    [SwaggerOperation(Summary = "Получить Facility по Id", Description = "Получает удобство по его идентификатору")]
    [SwaggerResponse(200, "Удобство найдено", typeof(FacilityEntityInfo))]
    [SwaggerResponse(404, "Удобство не найдено")]
    public async Task<IActionResult> GetByIdAsync([FromQuery] GetFacilityByIdQuery query, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);

        if (result.Value == null)
            return NotFound(result.Errors);

        return Ok(result.Value);
    }
}