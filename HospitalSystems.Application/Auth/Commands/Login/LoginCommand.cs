using HospitalSystems.Infrastructure.Auth;
using MediatR;

namespace HospitalSystems.Application.Auth.Commands.Login;

public record LoginCommand(String Email, String Password):IRequest<TokenResponse>;