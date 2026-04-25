using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HospitalSystems.Application.Admin.Commands.CreateRole;

public record CreateRoleCommand(string Name, List<string> Permissions) : IRequest<IdentityResult>;
