using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HospitalSystems.Application.Admin.Commands.RemovePermissionFromRole;

public record RemovePermissionFromRoleCommand(string RoleName, string Permission) : IRequest<IdentityResult>;
