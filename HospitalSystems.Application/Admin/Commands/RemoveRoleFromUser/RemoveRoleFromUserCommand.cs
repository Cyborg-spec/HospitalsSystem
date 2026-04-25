using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HospitalSystems.Application.Admin.Commands.RemoveRoleFromUser;

public record RemoveRoleFromUserCommand(Guid UserId, string RoleName) : IRequest<IdentityResult>;
