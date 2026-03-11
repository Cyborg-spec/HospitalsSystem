using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using HospitalSystems.Domain.Common.Interfaces;
using HospitalSystems.Domain.Constants;
using HospitalSystems.Domain.Enums;

namespace HospitalSystems.Presentation.Filters;

public class SameUserOrAdminAuthorizationFilter(IUserContext userContext) : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var routeIdStr = context.RouteData.Values["id"]?.ToString();
        
        if (string.IsNullOrEmpty(routeIdStr) || !Guid.TryParse(routeIdStr, out var requestedUserId))
        {
            context.Result = new BadRequestObjectResult(new { Error = "Invalid user ID" });
            return;
        }

        var currentUserId = userContext.UserId;
        var isUserAdmin = context.HttpContext.User.HasClaim("Permission", Permissions.Users.Manage);
        
        if (currentUserId != requestedUserId && !isUserAdmin)
        {
            context.Result = new ForbidResult();
        }
    }
}
