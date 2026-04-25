using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HospitalSystems.Application.Admin.Commands.AssignRoleToUser;

public record AssignRoleToUserCommand(Guid UserId, string RoleName) : IRequest<IdentityResult>;
