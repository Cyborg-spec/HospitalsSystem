using HospitalSystems.Domain.Common;

namespace HospitalSystems.Domain.Hospitals;

public class Department : AuditableEntity
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public string? Floor { get; private set; }
    public string? RoomNumber { get; private set; }
    public bool IsActive { get; private set; }

    // Foreign keys
    public Guid HospitalId { get; private set; }
    public Guid? HeadDoctorId { get; private set; }

    // Navigation properties
    public Hospital Hospital { get; private set; } = null!;


    private Department() { }


    public Department(string name, Guid hospitalId, string? description, string? floor, string? roomNumber)
    {
        Name = name;
        HospitalId = hospitalId;
        Description = description;
        Floor = floor;
        RoomNumber = roomNumber;
        IsActive = true;
    }
}