using Airbnb.Infrastructure.Entities;
using Airbnb.Infrastructure.Utils;
using AirbnbAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AirbnbAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthUserController : ControllerBase
    {
        private static List<UserEntity> users = new List<UserEntity>();
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            if(users.Any(u => u.Email == model.Email))
            {
                return BadRequest("User with this email already exists.");
            }
            var hashedPassword = PasswordHasher.HashPassword(model.Password);
            var user = new UserEntity
            {
                Id = Guid.NewGuid(),
                Email = model.Email,
                Password = hashedPassword // в бд попадает хэш
            };
            users.Add(user);
            return Ok(new { message = "User registered successfully", user.Id });
        }
    }
}
