using Microsoft.AspNetCore.Authorization;

namespace HospitalSystems.Infrastructure.Auth;

public class HasPermissionAttribute(string permission) : AuthorizeAttribute(permission)
{
}