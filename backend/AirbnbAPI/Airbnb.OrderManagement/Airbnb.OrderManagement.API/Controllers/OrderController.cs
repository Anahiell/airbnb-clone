using Microsoft.AspNetCore.Mvc;
using Airbnb.OrderManagement.Application.BoundedContext.Commands.CreateOrderCommand;
using Airbnb.OrderManagement.Application.BoundedContext.Commands.DeleteOrderCommand;
using Airbnb.OrderManagement.Application.BoundedContext.Commands.UpdateOrderCommand;
using Airbnb.OrderManagement.Application.BoundedContext.Queries.GetProductPaginatedQuery;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.OrderManagement.API.Controllers;

/// <summary>
/// Контроллер для взаимодействия с заказами
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class OrderController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Получить список заказов.
    /// </summary>
    [HttpGet]
    [Route("GetAllOrders")]
    [SwaggerOperation(Summary = "Получить заказы", 
        Description = "Получает список заказов с использованием пагинации и фильтрации.")]
    public async Task<IActionResult> GetAllOrdersAsync([FromQuery] GetOrderPaginatedQuery query, 
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result.Value);
    }

    /// <summary>
    /// Получить заказ по Id.
    /// </summary>
    [HttpGet]
    [Route("GetOrderById")]
    [SwaggerOperation(Summary = "Получить заказ по Id",
        Description = "Получает конкретный заказ по его идентификатору.")]
    public async Task<IActionResult> GetOrderByIdAsync([FromQuery] GetOrderPaginatedQuery query, 
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Создать заказ.
    /// </summary>
    [HttpPost]
    [Route("CreateOrder")]
    [SwaggerOperation(Summary = "Создать заказ", 
        Description = "Создает новый заказ.")]
    [SwaggerResponse(200, "Успешное создание", typeof(Guid))]
    [SwaggerResponse(400, "Ошибка валидации", typeof(string))]
    [SwaggerResponse(500, "Ошибка сервера", typeof(string))]
    public async Task<IActionResult> CreateOrderAsync([FromBody] CreateOrderCommand command, 
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result.Value);
    }

    /// <summary>
    /// Обновить заказ.
    /// </summary>
    [HttpPut]
    [Route("UpdateOrder")]
    [SwaggerOperation(Summary = "Обновить заказ", 
        Description = "Обновляет существующий заказ.")]
    public async Task<IActionResult> UpdateOrderAsync([FromBody] UpdateOrderCommand command, 
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Удалить заказ.
    /// </summary>
    [HttpDelete]
    [Route("DeleteOrder")]
    [SwaggerOperation(Summary = "Удалить заказ", 
        Description = "Удаляет заказ по идентификатору.")]
    public async Task<IActionResult> DeleteOrderAsync([FromBody] DeleteOrderCommand command, 
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }
}