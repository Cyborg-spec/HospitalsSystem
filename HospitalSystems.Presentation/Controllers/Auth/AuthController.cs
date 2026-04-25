using HospitalSystems.Application.Auth.Commands.Login;
using HospitalSystems.Application.Auth.Commands.Refresh;
using HospitalSystems.Application.Auth.Commands.Register;
using HospitalSystems.Application.Auth.Commands.Verify;
using HospitalSystems.Application.Common.Interfaces;
using HospitalSystems.Infrastructure.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HospitalSystems.Presentation.Controllers.Auth;

[ApiController]
[Route("api/auth")]
public class AuthController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost("login")]
    [ProducesResponseType(typeof(TokenResponse), 200)]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        try
        {
            var tokenResponse = await _sender.Send(command);
            return Ok(tokenResponse);
        }
        catch (Exception ex)
        {
            return Unauthorized(new { Error = ex.Message });
        }
    }
    [HttpPost("refresh")]
    [ProducesResponseType(typeof(TokenResponse), 200)]
    public async Task<IActionResult> Refresh([FromBody] RefreshCommand command)
    {
        try
        {
            var tokenResponse = await _sender.Send(command);
            return Ok(tokenResponse);
        }
        catch (Exception ex)
        {
            return Unauthorized(new { Error = ex.Message });
        }
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command)
    {
        var success = await _sender.Send(command);
        if (success)
        {
            return Ok(new { Message = "Registration successful. Please log in to complete 2FA." });
        }
        return BadRequest();
    }

    [HttpPost("verify-2fa")]
    public async Task<IActionResult> VerifyTwoFactor([FromBody] VerifyTwoFactorCommand command)
    {
        var result = await _sender.Send(command);
        return Ok(result);
    }
}
