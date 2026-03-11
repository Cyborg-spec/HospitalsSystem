using HospitalSystems.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystems.Application.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler(UserManager<User> userManager) : IRequestHandler<GetUserByIdQuery, UserDto>
{
    private readonly UserManager<User> _userManager = userManager;

    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        return new UserDto(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email!,
            user.HospitalId,
            user.DepartmentId,
            user.IsActive
        );
    }
}