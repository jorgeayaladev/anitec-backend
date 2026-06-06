using System.Net.Mime;
using Titan.AniTec.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Titan.AniTec.Platform.Iam.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/identity")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Session and Security endpoints")]
public class SessionsController : ControllerBase
{
    [HttpGet("sessions")]
    [SwaggerOperation(Summary = "List active sessions", OperationId = "GetActiveSessions")]
    [SwaggerResponse(StatusCodes.Status200OK, "Active sessions retrieved")]
    public async Task<IActionResult> GetActiveSessions()
    {
        return Ok(new List<object>());
    }

    [HttpDelete("sessions/{sessionId}")]
    [SwaggerOperation(Summary = "Close specific session", OperationId = "CloseSession")]
    [SwaggerResponse(StatusCodes.Status200OK, "Session closed")]
    public async Task<IActionResult> CloseSession(string sessionId)
    {
        return Ok(new { message = "Session closed" });
    }

    [HttpDelete("sessions")]
    [SwaggerOperation(Summary = "Close all other sessions", OperationId = "CloseAllOtherSessions")]
    [SwaggerResponse(StatusCodes.Status200OK, "All other sessions closed")]
    public async Task<IActionResult> CloseAllOtherSessions()
    {
        return Ok(new { message = "All other sessions closed" });
    }

    [HttpPost("enable-2fa")]
    [SwaggerOperation(Summary = "Enable 2FA", OperationId = "Enable2FA")]
    [SwaggerResponse(StatusCodes.Status200OK, "2FA enabled")]
    public async Task<IActionResult> Enable2FA()
    {
        return Ok(new { message = "2FA enabled", sharedKey = "XXXX-XXXX-XXXX-XXXX", qrCodeUrl = "" });
    }

    [HttpPost("disable-2fa")]
    [SwaggerOperation(Summary = "Disable 2FA", OperationId = "Disable2FA")]
    [SwaggerResponse(StatusCodes.Status200OK, "2FA disabled")]
    public async Task<IActionResult> Disable2FA()
    {
        return Ok(new { message = "2FA disabled" });
    }

    [HttpGet("2fa-status")]
    [SwaggerOperation(Summary = "Check 2FA status", OperationId = "Get2FAStatus")]
    [SwaggerResponse(StatusCodes.Status200OK, "2FA status retrieved")]
    public async Task<IActionResult> Get2FAStatus()
    {
        return Ok(new { isEnabled = false });
    }

    [HttpPost("verify-2fa")]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Verify 2FA code", OperationId = "Verify2FA")]
    [SwaggerResponse(StatusCodes.Status200OK, "2FA code verified")]
    public async Task<IActionResult> Verify2FA([FromBody] Verify2FARequest request)
    {
        return Ok(new { isValid = true, token = "" });
    }
}

public record Verify2FARequest(string Code, string? SharedKey);
