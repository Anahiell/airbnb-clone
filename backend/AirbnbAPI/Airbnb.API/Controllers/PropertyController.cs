using Airbnb.Infrastructure.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirbnbAPI.Controllers
{
    [Route("api/properties")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllProperties()
        {
            return Ok();
        }
        Random rnd;
        [HttpPost]
        public IActionResult CreateProperty([FromBody] PropertyEntity property)
        {
            property.Id = rnd.Next()+5;

            return Ok(property);
        }
    }
}
