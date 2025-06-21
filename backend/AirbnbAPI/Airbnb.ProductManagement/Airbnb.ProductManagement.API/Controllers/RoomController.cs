using Airbnb.ProductManagement.Application.BoundedContext.ProductFeatureManagement.Commands.CreateFeatureCommand;
using Airbnb.ProductManagement.Application.BoundedContext.ProductFeatureManagement.Commands.DeleteFeatureCommand;
using Airbnb.ProductManagement.Application.BoundedContext.ProductFeatureManagement.Commands.UpdateFeatureCommand;
using Airbnb.ProductManagement.Application.BoundedContext.RoomManagement.Commands.CreateRoomCommand;
using Airbnb.ProductManagement.Application.BoundedContext.RoomManagement.Commands.DeleteRoomCommand;
using Airbnb.ProductManagement.Application.BoundedContext.RoomManagement.Commands.UpdateRoomCommand;
using Airbnb.ProductManagement.Application.BoundedContext.RoomManagement.Queries.GetAllRoomsQuery;
using Airbnb.ProductManagement.Application.BoundedContext.RoomManagement.Queries.GetRoomByIdQuery;
using Airbnb.ProductManagement.Application.BoundedContext.RoomManagement.QueryObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AirbnbAPI.Controllers;

/// <summary>
/// Контроллер для взаимодействия с комнатами (Room)
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class RoomController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Создать новую комнату (Room).
    /// </summary>
    [HttpPost]
    [Route("CreateRoomAsync")]
    [SwaggerOperation(Summary = "Создать Room", Description = "Создает новую комнату")]
    [SwaggerResponse(200, "Успешно создано", typeof(int))]
    [SwaggerResponse(400, "Ошибка валидации")]
    public async Task<IActionResult> CreateRoomAsync([FromQuery] CreateRoomCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result.Value);
    }

    /// <summary>
    /// Обновить существующую комнату (Room).
    /// </summary>
    [HttpPut]
    [Route("UpdateRoomAsync")]
    [SwaggerOperation(Summary = "Обновить Room", Description = "Обновляет комнату")]
    [SwaggerResponse(200, "Успешно обновлено")]
    [SwaggerResponse(400, "Ошибка валидации")]
    public async Task<IActionResult> UpdateRoomAsync([FromQuery] UpdateRoomCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Удалить комнату (Room).
    /// </summary>
    [HttpDelete]
    [Route("DeleteRoomAsync")]
    [SwaggerOperation(Summary = "Удалить Room", Description = "Удаляет комнату по идентификатору")]
    [SwaggerResponse(200, "Успешно удалено")]
    [SwaggerResponse(404, "Feature не найден")]
    public async Task<IActionResult> DeleteRoomAsync([FromQuery] DeleteRoomCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Получить список всех комнат (Room).
    /// </summary>
    [HttpGet]
    [Route("GetAll")]
    [SwaggerOperation(Summary = "Получить все Room", Description = "Получает все комнаты из базы данных")]
    [SwaggerResponse(200, "Список Room", typeof(List<RoomEntityInfo>))]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var query = new GetAllRoomsQuery();
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result.Value);
    }

    /// <summary>
    /// Получить комнату по Id.
    /// </summary>
    [HttpGet]
    [Route("GetById")]
    [SwaggerOperation(Summary = "Получить Room по Id", Description = "Получает комнату по её идентификатору")]
    [SwaggerResponse(200, "Комната найдена", typeof(RoomEntityInfo))]
    [SwaggerResponse(404, "Комната не найдена")]
    public async Task<IActionResult> GetByIdAsync([FromQuery] GetRoomByIdQuery query,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);

        if (result.Value == null)
            return NotFound(result.Errors);

        return Ok(result.Value);
    }
}