using Microsoft.AspNetCore.Mvc;
using Airbnb.PictureManagement.Application.BoundedContext.Commands;
using Airbnb.PictureManagement.Application.BoundedContext.Queries;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.PictureManagement.API.Controllers;

    /// <summary>
    /// Контроллер для взаимодействия с Картинками
    /// </summary>
    /// <param name="mediator"></param>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PictureController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Route("/GetHelloPicture")]
        public IActionResult GetHelloPicture()
        {
            return Ok(new { message = "Привет из BACKEND .NET API - Picture Management!" });
        }

        /// <summary>
        /// Получить список всех картинок.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET http://localhost:8080/GetAllPicture
        /// </remarks>
        /// <returns>Список картинок</returns>
        [HttpGet]
        [Route("/GetAllPicture")]
        [SwaggerOperation(Summary = "Получить все картинки",
            Description = "Получает все картинки с пагинацией и фильтрацией")]
        public async Task<IActionResult> GetAllPictureAsync([FromQuery] GetPicturePaginatedQuery query,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(query, cancellationToken);

            return Ok(result.Value);
        }

        /// <summary>
        /// Получить картинку по Id.
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET http://localhost:8080/GetPictureByIdAsync
        /// </remarks>
        [HttpGet]
        [Route("/GetPictureByIdAsync")]
        [SwaggerOperation(Summary = "Получить картинку по Id",
            Description = "Получает картинку по её идентификатору")]
        public async Task<IActionResult> GetPictureByIdAsync([FromQuery] GetPicturePaginatedQuery query,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(query, cancellationToken);

            return Ok(result.Value);
        }

        /// <summary>
        /// Создать картинку.
        /// </summary>
        [HttpPost]
        [Route("/CreatePictureAsync")]
        [SwaggerOperation(Summary = "Создать картинку", Description = "Создаёт новую картинку")]
        [SwaggerResponse(200, "Успешный запрос", typeof(string))]
        [SwaggerResponse(400, "Ошибка Валидации", typeof(string))]
        [SwaggerResponse(500, "Ошибка Сервера", typeof(string))]
        public async Task<IActionResult> CreatePictureAsync([FromQuery] CreatePictureCommand command,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);

            return Ok(result.Value);
        }

        /// <summary>
        /// Обновить картинку.
        /// </summary>
        [HttpPost]
        [Route("/UpdatePictureAsync")]
        [SwaggerOperation(Summary = "Обновить картинку",
            Description = "Обновляет существующую картинку")]
        public async Task<IActionResult> UpdatePictureAsync([FromBody] CreatePictureCommand command,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);

            return Ok(result.Value);
        }

        /// <summary>
        /// Удалить картинку.
        /// </summary>
        [HttpPost]
        [Route("/DeletePictureAsync")]
        [SwaggerOperation(Summary = "Удалить картинку",
            Description = "Удаляет картинку из базы данных")]
        public async Task<IActionResult> DeletePictureAsync([FromBody] CreatePictureCommand command,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);

            return Ok(result.Value);
        }

        /// <summary>
        /// Сделать картинку архивной.
        /// </summary>
        [HttpPost]
        [Route("/ArchivePictureAsync")]
        [SwaggerOperation(Summary = "Архивировать картинку",
            Description = "Архивирует картинку (не удаляет физически из базы данных)")]
        public async Task<IActionResult> ArchivePictureAsync([FromBody] CreatePictureCommand command,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);

            return Ok(result.Value);
        }
    }