using HospitalSystems.Application.Users.Queries;
using HospitalSystems.Application.Users.Queries.GetUserById;
using HospitalSystems.Domain.Common.Interfaces;
using HospitalSystems.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace HospitalSystems.Presentation.Controllers.Users;


[ApiController]
[Route("api/users")]
public class UserController:ControllerBase
{
    private readonly ISender _sender;
    private readonly IUserContext _userContext;
    public UserController(ISender sender, IUserContext userContext)
    {
        _sender = sender;
        _userContext = userContext;
    }

    [HttpGet]
    [Authorize(Policy = "RequiresAdmin")]
    [ProducesResponseType(typeof(List<UserDto>), 200)]
    public async Task<IActionResult> GetUsers([FromQuery] GetUsersQuery query)
    {
        var users = await _sender.Send(query);
        return Ok(users);
    }

    [HttpGet("{id}")]
    [Authorize] //any user can access this but we manually restrict the returned data
    [ProducesResponseType(typeof(UserDto), 200)]
    public async Task<IActionResult> GetUser([FromRoute] Guid id)
    {
        var currentUserId = _userContext.UserId;
        var isUserAdmin = User.IsInRole(nameof(UserRole.SuperAdmin)) || 
                          User.IsInRole(nameof(UserRole.HospitalAdmin));
        
        if (currentUserId != id && !isUserAdmin)
        {
            return Forbid();
        }

        try
        {
            var query = new GetUserByIdQuery(id);
            var user = await _sender.Send(query);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return NotFound(new { Error = ex.Message }); 
        }
    }
}