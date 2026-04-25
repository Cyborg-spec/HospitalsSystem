using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HospitalSystems.Application.Admin.Commands.AddPermissionToRole;

public record AddPermissionToRoleCommand(string RoleName, string Permission) : IRequest<IdentityResult>;
