using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskHub.Infrastructure.Identity;

namespace TaskHub.Api.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController(
    UserManager<AppUser> userManager
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
}

public sealed record RegisterRequest(
    [Required]
    [ MaxLength(100)]
    string DisplayName,

    [Required]
    [EmailAddress]
    string Email,

    [Required]
    [MinLength(8)]
    string Password
);

public sealed record RegisterResponse(
    Guid Id,
    string DisplayName,
    string Email
);