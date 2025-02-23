using Airbnb.Infrastructure.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirbnbAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok();
        }
        [HttpPost]
        public IActionResult CreateUser([FromBody] UserEntity user)
        {
            user.Id = Guid.NewGuid();
            //  добавить нового юзера в бд
            return Ok(user);
        }
    }
}
