using System.Collections.Immutable;

namespace HospitalSystems.Application.Common.Interfaces;

public interface IPermissionService
{
    Task<HashSet<string>> GetPermissionsAsync(Guid userId);
}
