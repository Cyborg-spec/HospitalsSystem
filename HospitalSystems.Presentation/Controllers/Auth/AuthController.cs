using HospitalSystems.Application.Auth.Commands;
using HospitalSystems.Application.Auth.Commands.Login;
using HospitalSystems.Application.Auth.Commands.Refresh;
using HospitalSystems.Application.Auth.Commands.Register;
using HospitalSystems.Infrastructure.Auth;
using Microsoft.AspNetCore.Mvc;

namespace HospitalSystems.Presentation.Controllers;

using MediatR;

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
            // Send the command to our LoginCommandHandler and wait for the TokenResponse!
            var tokenResponse = await _sender.Send(command);
            
            // Return 200 OK with the generated TokenResponse structure as JSON
            return Ok(tokenResponse);
        }
        catch (Exception ex)
        {
            // If the user's password was wrong, the Handler "throws", and we catch it here to return a 401 Unauthorized
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
            // If the refresh token is invalid or expired, return 401. 
            // This tells the frontend app "You must force the user to log in manually again!"
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