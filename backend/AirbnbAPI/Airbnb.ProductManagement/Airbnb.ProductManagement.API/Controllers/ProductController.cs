using Airbnb.ProductManagement.Application.BoundedContext.Commands.CreateProduct;
using Airbnb.ProductManagement.Application.BoundedContext.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AirbnbAPI.Controllers
{
    /// <summary>
    /// Контроллер для взаимодействия с Объектами недвижимости
    /// </summary>
    /// <param name="mediator"></param>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Route("GetHello")]
        public IActionResult GetHello()
        {
            return Ok(new { message = "Привет из BACKEND .NET API!" });
        }

        /// <summary>
        /// Получить список всех объектов недвижимости.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET http://localhost:8080/GetAllPropertyEntity
        /// </remarks>
        /// <returns>Список объектов недвижимости</returns>
        [HttpGet]
        [Route("GetAllProperty")]
        [SwaggerOperation(Summary = "Получить объекты недвижимости",
            Description = "Получает объекты недвижимости из базы данных с использованием паггинации и фильтрации")]
        public async Task<IActionResult> GetAllPropertyAsync([FromQuery] GetProductPaginatedQuery query,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(query, cancellationToken);

            return Ok(result.Value);
        }

        /// <summary>
        /// Получить объект недвижимости по Id.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET http://localhost:8080/GetAllPropertyEntity
        /// </remarks>
        /// <returns>Список объектов недвижимости</returns>
        [HttpGet]
        [Route("GetPropertyByIdAsync")]
        [SwaggerOperation(Summary = "Получить объект недвижимости по Id",
            Description = "Получает объект недвижимости из базы данных по его идентификатору")]
        public async Task<IActionResult> GetPropertyByIdAsync([FromQuery] GetProductPaginatedQuery query,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        /// <summary>
        /// Создать объект недвижимости.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreatePropertyAsync")]
        [SwaggerOperation(Summary = "Создать объект недвижимости", Description = "Создать объект недвижимости")]
        [SwaggerResponse(200, "Успешный запрос", typeof(int))]
        [SwaggerResponse(400, "Ошибка Валидации", typeof(int))]
        [SwaggerResponse(500, "Ошибка Сервера", typeof(int))]
        public async Task<IActionResult> CreatePropertyAsync([FromQuery] CreateProductCommand command,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);

            return Ok(result.Value);
        }

        /// <summary>
        /// Обновить объект недвижимости.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdatePropertyAsync")]
        [SwaggerOperation(Summary = "Обновить объект недвижимости",
            Description = "Обновляет объект недвижимости из базы данных.")]
        public async Task<IActionResult> UpdatePropertyAsync([FromBody] CreateProductCommand command,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);

            return Ok(result.Value);
        }

        /// <summary>
        /// Удалить объект недвижимости.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeletePropertyAsync")]
        [SwaggerOperation(Summary = "Удалить объект недвижимости",
            Description = "Удаляет объект недвижимости из базы данных.")]
        public async Task<IActionResult> DeletePropertyAsync([FromBody] CreateProductCommand command,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);

            return Ok(result.Value);
        }

        /// <summary>
        /// Архивирует объект недвижимости.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ArchivePropertyAsync")]
        [SwaggerOperation(Summary = "Архивирует объект недвижимости",
            Description = "Архивирует объект недвижимости ( не удаляет из базы данных )")]
        public async Task<IActionResult> ArchivePropertyAsync([FromBody] CreateProductCommand command,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);

            return Ok(result.Value);
        }
    }
}