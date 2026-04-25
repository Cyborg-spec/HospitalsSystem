using HospitalSystems.Application.Common.Interfaces;
using MediatR;

namespace HospitalSystems.Application.Auth.Commands.Refresh;

public record RefreshCommand(string RefreshToken):IRequest<TokenResponse>;