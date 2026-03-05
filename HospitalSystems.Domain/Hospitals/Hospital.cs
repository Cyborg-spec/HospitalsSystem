using HospitalSystems.Domain.Common;
using HospitalSystems.Domain.Enums;

namespace HospitalSystems.Domain.Hospitals;

public class Hospital : AuditableEntity
{
    public string Name { get; private set; }
    public string Address { get; private set; }
    public string City { get; private set; }
    public string PhoneNumber { get; private set; }
    public string? Email { get; private set; }
    public HospitalType Type { get; private set; }
    public bool IsActive { get; private set; }

    // Navigation properties
    public ICollection<Department> Departments { get; private set; } = new List<Department>();
    
    private Hospital() { }

    public Hospital(string name, string address, string city, string phoneNumber, string? email, HospitalType type)
    {
        Name = name;
        Address = address;
        City = city;
        PhoneNumber = phoneNumber;
        Email = email;
        Type = type;
        IsActive = true;
        
    }
}