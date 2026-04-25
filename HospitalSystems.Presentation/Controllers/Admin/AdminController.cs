using HospitalSystems.Application.Admin.Commands.AddPermissionToRole;
using HospitalSystems.Application.Admin.Commands.AssignRoleToUser;
using HospitalSystems.Application.Admin.Commands.CreateRole;
using HospitalSystems.Application.Admin.Commands.DeleteRole;
using HospitalSystems.Application.Admin.Commands.RemovePermissionFromRole;
using HospitalSystems.Application.Admin.Commands.RemoveRoleFromUser;
using HospitalSystems.Application.Admin.Queries.GetAllRoles;
using HospitalSystems.Application.Admin.Queries.GetRolePermissions;
using HospitalSystems.Application.Admin.Queries.GetUserRoles;
using HospitalSystems.Domain.Constants;
using HospitalSystems.Infrastructure.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HospitalSystems.Presentation.Controllers.Admin;

[ApiController]
[Route("api/admin")]
[HasPermission(Permissions.Users.Manage)]
public class AdminController(ISender sender) : ControllerBase
{

    [HttpPost("roles")]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleCommand command)
    {
        var result = await sender.Send(command);
        if (!result.Succeeded)
            return BadRequest(new { Errors = result.Errors.Select(e => e.Description) });

        return Ok(new { Message = $"Role '{command.Name}' created successfully." });
    }

    [HttpDelete("roles/{roleName}")]
    public async Task<IActionResult> DeleteRole([FromRoute] string roleName)
    {
        var result = await sender.Send(new DeleteRoleCommand(roleName));
        if (!result.Succeeded)
            return BadRequest(new { Errors = result.Errors.Select(e => e.Description) });

        return Ok(new { Message = $"Role '{roleName}' deleted successfully." });
    }

    [HttpGet("roles")]
    public async Task<IActionResult> GetAllRoles()
    {
        var roles = await sender.Send(new GetAllRolesQuery());
        return Ok(roles);
    }

    [HttpGet("roles/{roleName}/permissions")]
    public async Task<IActionResult> GetRolePermissions([FromRoute] string roleName)
    {
        try
        {
            var permissions = await sender.Send(new GetRolePermissionsQuery(roleName));
            return Ok(permissions);
        }
        catch (Exception ex)
        {
            return NotFound(new { Error = ex.Message });
        }
    }

    [HttpPost("roles/permissions")]
    public async Task<IActionResult> AddPermissionToRole([FromBody] AddPermissionToRoleCommand command)
    {
        var result = await sender.Send(command);
        if (!result.Succeeded)
            return BadRequest(new { Errors = result.Errors.Select(e => e.Description) });

        return Ok(new { Message = $"Permission '{command.Permission}' added to role '{command.RoleName}'." });
    }

    [HttpDelete("roles/permissions")]
    public async Task<IActionResult> RemovePermissionFromRole([FromBody] RemovePermissionFromRoleCommand command)
    {
        var result = await sender.Send(command);
        if (!result.Succeeded)
            return BadRequest(new { Errors = result.Errors.Select(e => e.Description) });

        return Ok(new { Message = $"Permission '{command.Permission}' removed from role '{command.RoleName}'." });
    }

    [HttpPost("users/roles")]
    public async Task<IActionResult> AssignRoleToUser([FromBody] AssignRoleToUserCommand command)
    {
        var result = await sender.Send(command);
        if (!result.Succeeded)
            return BadRequest(new { Errors = result.Errors.Select(e => e.Description) });

        return Ok(new { Message = $"Role '{command.RoleName}' assigned to user '{command.UserId}'." });
    }

    [HttpDelete("users/roles")]
    public async Task<IActionResult> RemoveRoleFromUser([FromBody] RemoveRoleFromUserCommand command)
    {
        var result = await sender.Send(command);
        if (!result.Succeeded)
            return BadRequest(new { Errors = result.Errors.Select(e => e.Description) });

        return Ok(new { Message = $"Role '{command.RoleName}' removed from user '{command.UserId}'." });
    }

    [HttpGet("users/{userId:guid}/roles")]
    public async Task<IActionResult> GetUserRoles([FromRoute] Guid userId)
    {
        try
        {
            var roles = await sender.Send(new GetUserRolesQuery(userId));
            return Ok(roles);
        }
        catch (Exception ex)
        {
            return NotFound(new { Error = ex.Message });
        }
    }
}
