using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Airbnb.Infrastructure.DataContext;
using Airbnb.Infrastructure.Entities;
using Airbnb.Infrastructure.Utils;
using AirbnbAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Airbnb.UserManagement.API.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthUserController : ControllerBase
{
    private readonly AirbnbDbContext _context;
    private readonly string _jwtSecret = "super_secret_key_12345"; //временный ключ

    public AuthUserController(AirbnbDbContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterModel model)
    {
        if (_context.Users.Any(u => u.Email == model.Email))
        {
            return BadRequest("User with this email alreade exists.");
        }

        var hashedPassword = PasswordHasher.HashPassword(model.Password);
        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = model.Email,
            Password = hashedPassword
        };
        _context.Users.Add(user);
        _context.SaveChanges();
        return Ok(new { message = "User registered successfully", user.Id });
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] RegisterModel model)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == model.Email);
        if (user == null || !PasswordHasher.VerifyPassword(model.Password, user.Password))
        {
            return Unauthorized(new { message = "Invalid email or password" });
        }

        var token = GenerateJwtToken(user);
        return Ok(new { token });
    }

    private string GenerateJwtToken(UserEntity user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        var token = new JwtSecurityToken(
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: credentials,
            claims: claims);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}