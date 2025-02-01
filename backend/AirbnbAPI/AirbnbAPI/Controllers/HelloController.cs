using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirbnbAPI.Controllers
{
    [Route("api/hello")]
    [ApiController]
    public class HelloController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetHello()
        {
            return Ok(new { message = "Привет из BACKEND .NET API!" });
        }
    }
}
