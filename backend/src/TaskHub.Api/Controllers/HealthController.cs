using Microsoft.AspNetCore.Mvc;

namespace TaskHub.Api.Controllers;

[ApiController]
[Route("api/health")]
public sealed class HealthController : ControllerBase
{
    [HttpGet]
    public ActionResult<HealthResponse> Get()
    {
        var response = new HealthResponse(
            Status: "healthy",
            Service: "TaskHub.Api",
            UtcTime: DateTimeOffset.UtcNow
        );

        return Ok(response);
    }
}

public sealed record HealthResponse(
    string Status,
    string Service,
    DateTimeOffset UtcTime
);