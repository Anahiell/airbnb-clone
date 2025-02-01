using Airbnb.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AirbnbAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HelloController(IPropertyEntityRepository propertyEntityRepository) : ControllerBase
    {
        [HttpGet]
        [Route("/GetHello")]
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
        [Route("/GetAllPropertyEntity")]
        [SwaggerOperation(Summary = "Получить все объекты недвижимости", Description = "Получает все объекты недвижимости из базы данных.")]
        public async Task<IActionResult> GetAllPropertyEntity()
        {
            var properties = await propertyEntityRepository.GetAllPropertyEntity();
            return Ok(properties);
        }
    }
}
