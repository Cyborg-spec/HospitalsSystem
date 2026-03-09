using HospitalSystems.Application.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace HospitalSystems.Presentation.Controllers.Users;


[ApiController]
[Route("api/users")]
public class UserController:ControllerBase
{
    private readonly ISender _sender;
    public UserController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    [Authorize(Policy = "RequiresAdmin")]
    [ProducesResponseType(typeof(List<UserDto>), 200)]
    public async Task<IActionResult> GetUsers([FromQuery] GetUsersQuery query)
    {
        var users = await _sender.Send(query);
        return Ok(users);
    }
}