using System.Security.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Airbnb.UserManagement.API.Controllers;

[AllowAnonymous]
public class AuthenticationController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IAuthenticationRepository _authRepository;

    public AuthenticationController(IMediator mediator, IAuthenticationRepository authRepository)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _authRepository = authRepository ?? throw new ArgumentNullException(nameof(authRepository));
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("ConfirmEmail")]
    public async Task<IActionResult> VerifyEmailAddress(Guid userId, string token)
    {
        var verificationResult = await _authRepository.IsValidEmailVerificationToken(userId, token);
        if (verificationResult is false)
            return BadRequest();

        if (await _authRepository.UserHasRole(userId, FixedRoles.AdminRole))
        {
            await _mediator.Send(new AdminAccountVerifyCommand
            {
                UserId = userId
            });

            return Ok();
        }

        if (await _authRepository.UserHasRole(userId, FixedRoles.CustomerRole))
        {
            await _mediator.Send(new CustomerAccountVerifyCommand
            {
                UserId = userId
            });

            return Ok();
        }

        return BadRequest();
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] LoginCredentialsDTO dto)
    {
        try
        {
            var token = await _authRepository.GenerateJWT(dto.Username, dto.Password);
            var json = JsonConvert.SerializeObject(new { token });
            return Content(json, "application/json");
        }
        catch (AuthenticationException ex)
        {
            var errorJson = JsonConvert.SerializeObject(new { message = ex.Message });
            return Content(errorJson, "application/json");
        }
    }
}