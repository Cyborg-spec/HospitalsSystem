using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HospitalSystems.Application.Admin.Commands.DeleteRole;

public record DeleteRoleCommand(string RoleName) : IRequest<IdentityResult>;
