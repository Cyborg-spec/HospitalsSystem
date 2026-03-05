using HospitalSystems.Domain.Common;
using HospitalSystems.Domain.Enums;

namespace HospitalSystems.Domain.Patients;

public class Patient : AuditableEntity
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string NationalId { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public Gender Gender { get; private set; }
    public BloodType? BloodType { get; private set; }
    public string PhoneNumber { get; private set; }
    public string? Email { get; private set; }
    public string Address { get; private set; }
    public string? Allergies { get; private set; }
    public string? EmergencyContactName { get; private set; }
    public string? EmergencyContactPhone { get; private set; }

    private Patient() { }

    public Patient(string firstName, string lastName, string nationalId, DateTime dateOfBirth,
        Gender gender, BloodType? bloodType, string phoneNumber, string? email, string address,
        string? allergies, string? emergencyContactName, string? emergencyContactPhone)
    {
        FirstName = firstName;
        LastName = lastName;
        NationalId = nationalId;
        DateOfBirth = dateOfBirth;
        Gender = gender;
        BloodType = bloodType;
        PhoneNumber = phoneNumber;
        Email = email;
        Address = address;
        Allergies = allergies;
        EmergencyContactName = emergencyContactName;
        EmergencyContactPhone = emergencyContactPhone;
    }
}