using HospitalSystems.Application.Users.Queries;
using HospitalSystems.Application.Users.Queries.GetUserById;
using HospitalSystems.Domain.Common.Interfaces;
using HospitalSystems.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HospitalSystems.Presentation.Filters;

namespace HospitalSystems.Presentation.Controllers.Users;


[ApiController]
[Route("api/users")]
public class UserController(ISender sender) : ControllerBase
{

    [HttpGet]
    [Authorize(Policy = "RequiresAdmin")]
    [ProducesResponseType(typeof(List<UserDto>), 200)]
    public async Task<IActionResult> GetUsers([FromQuery] GetUsersQuery query)
    {
        var users = await sender.Send(query);
        return Ok(users);
    }

    [HttpGet("{id}")]
    [Authorize] //any user can access this but we manually restrict the returned data
    [TypeFilter(typeof(SameUserOrAdminAuthorizationFilter))]
    [ProducesResponseType(typeof(UserDto), 200)]
    public async Task<IActionResult> GetUser([FromRoute] Guid id)
    {

        try
        {
            var query = new GetUserByIdQuery(id);
            var user = await sender.Send(query);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return NotFound(new { Error = ex.Message }); 
        }
    }
}