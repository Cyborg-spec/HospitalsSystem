using HospitalSystems.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystems.Application.Users.Queries;

public class GetUsersQueryHandler(UserManager<User> userManager) : IRequestHandler<GetUsersQuery, List<UserDto>>
{
    public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var query = userManager.Users.AsNoTracking();
        if (request.HospitalId.HasValue)
        {
            query = query.Where(u => u.HospitalId == request.HospitalId.Value);
        }
        if (request.DepartmentId.HasValue)
        {
            query = query.Where(u => u.DepartmentId == request.DepartmentId.Value);
        }

        var users = await query
            .Select(u => new UserDto(
                u.Id,
                u.FirstName,
                u.LastName,
                u.Email!, 
                u.Role,
                u.HospitalId,
                u.DepartmentId,
                u.IsActive
            ))
            .ToListAsync(cancellationToken);

        return users;
    }
}