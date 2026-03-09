using HospitalSystems.Application.Auth.Commands.Login;
using HospitalSystems.Application.Auth.Commands.Refresh;
using HospitalSystems.Application.Auth.Commands.Register;
using HospitalSystems.Infrastructure.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HospitalSystems.Presentation.Controllers.Auth;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly ISender _sender;
    public AuthController(ISender sender)
    {
        _sender = sender;
    }
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
    [ProducesResponseType(typeof(TokenResponse), 200)]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command)
    {
        try
        {
            var tokenResponse = await _sender.Send(command);
            return Ok(tokenResponse);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
}