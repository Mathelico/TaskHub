using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskHub.Infrastructure.Identity;

namespace TaskHub.Api.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController(
    UserManager<AppUser> userManager,
    SignInManager<AppUser> signInManager
) : ControllerBase
{
    [HttpPost("register")]
    [ProducesResponseType<RegisterResponse>(
        StatusCodes.Status201Created
    )]
    [ProducesResponseType<ValidationProblemDetails>(
        StatusCodes.Status400BadRequest
    )]
    public async Task<ActionResult<RegisterResponse>> Register(
        RegisterRequest request
    )
    {
        var user = new AppUser
        {
            Id = Guid.NewGuid(),
            DisplayName = request.DisplayName.Trim(),
            Email = request.Email.Trim(),
            UserName = request.Email.Trim()
        };

        var result = await userManager.CreateAsync(
            user,
            request.Password
        );

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(
                    error.Code,
                    error.Description
                );
            }

            return ValidationProblem(ModelState);
        }

        var response = new RegisterResponse(
            user.Id,
            user.DisplayName,
            user.Email!
        );

        return StatusCode(
            StatusCodes.Status201Created,
            response
        );
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ProblemDetails>(
        StatusCodes.Status401Unauthorized
    )]
    public async Task<IActionResult> Login(
        LoginRequest request
    )
    {
        var result = await signInManager.PasswordSignInAsync(
            request.Email.Trim(),
            request.Password,
            request.RememberMe,
            lockoutOnFailure: true
        );

        if (!result.Succeeded)
        {
            var problem = new ProblemDetails
            {
                Status = StatusCodes.Status401Unauthorized,
                Title = "Não foi possível entrar.",
                Detail = "E-mail ou senha inválidos."
            };

            return Unauthorized(problem);
        }

        return NoContent();
    }

    [Authorize]
    [HttpGet("me")]
    [ProducesResponseType<CurrentUserResponse>(
        StatusCodes.Status200OK
    )]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<CurrentUserResponse>> Me()
    {
        var user = await userManager.GetUserAsync(User);

        if (user is null)
        {
            return Unauthorized();
        }

        var response = new CurrentUserResponse(
            user.Id,
            user.DisplayName,
            user.Email!
        );

        return Ok(response);
    }
}

public sealed record RegisterRequest(
    [Required, MaxLength(100)]
    string DisplayName,

    [Required, EmailAddress]
    string Email,

    [Required, MinLength(8)]
    string Password
);

public sealed record LoginRequest(
    [Required, EmailAddress]
    string Email,

    [Required]
    string Password,

    bool RememberMe = false
);

public sealed record CurrentUserResponse(
    Guid Id,
    string DisplayName,
    string Email
);

public sealed record RegisterResponse(
    Guid Id,
    string DisplayName,
    string Email
);
