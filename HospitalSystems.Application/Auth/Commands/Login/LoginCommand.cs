using HospitalSystems.Application.Auth.Commands.Common;
using HospitalSystems.Application.Common.Interfaces;
using MediatR;

namespace HospitalSystems.Application.Auth.Commands.Login;

public record LoginCommand(string Email, string Password):IRequest<LoginResult>;
