using Airbnb.Infrastructure.DataContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Airbnb.UserManagement.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize] // требуется авторизация через JWT
    public class UserController : ControllerBase
    {
        private readonly AirbnbDbContext _context;
        public UserController(AirbnbDbContext context)
        {
            _context = context;
        }

        //[GET] /api/users
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _context.Users.Select(u => new { u.Id, u.Email }).ToList();
            return Ok(users);
        }

        //[GET] /api/users/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();

            return Ok(new { user.Id, user.Email });
        }
        // [PUT] /api/users/{id}
        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] string newEmail)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();

            user.Email = newEmail;
            _context.SaveChanges();

            return Ok(new { message = "User updated", user.Id, user.Email });
        }

        // [DELETE] /api/users/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();

            _context.Users.Remove(user);
            _context.SaveChanges();

            return Ok(new { message = "User deleted", user.Id });
        }
    }
}
