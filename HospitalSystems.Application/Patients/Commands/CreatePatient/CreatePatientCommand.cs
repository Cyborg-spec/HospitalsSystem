using HospitalSystems.Domain.Enums;
using MediatR;

namespace HospitalSystems.Application.Patients.Commands.CreatePatient;

public record CreatePatientCommand(
    String FirstName,
    String LastName,
    String NationalId,
    DateTime DateOfBirth,
    Gender Gender,
    BloodType? BloodType,
    string PhoneNumber,
    String? EmailAddress,
    String Address,
    string? Allergies,
    string? EmergencyContactName,
    string? EmergencyContactPhone):IRequest<Guid>;