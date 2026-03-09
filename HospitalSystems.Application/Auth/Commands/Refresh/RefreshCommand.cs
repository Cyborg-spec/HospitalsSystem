using HospitalSystems.Infrastructure.Auth;
using MediatR;

namespace HospitalSystems.Application.Auth.Commands.Refresh;

public record RefreshCommand(string RefreshToken):IRequest<TokenResponse>;