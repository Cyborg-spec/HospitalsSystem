using HospitalSystems.Application.Auth.Commands.Common;
using HospitalSystems.Application.Auth.Commands.Login;
using MediatR;

namespace HospitalSystems.Application.Auth.Commands.Verify;

public record VerifyTwoFactorCommand(Guid UserId, string Code):IRequest<LoginResult>
{

}
