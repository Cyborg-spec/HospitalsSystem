using HospitalSystems.Domain.Common.Interfaces;
using HospitalSystems.Domain.Patients;
using MediatR;

namespace HospitalSystems.Application.Patients.Commands.CreatePatient;

public class CreatePatientCommandHandler(IPatientRepository patientRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreatePatientCommand, Guid>
{
    private readonly IPatientRepository _patientRepository = patientRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Guid> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
    {
        var patient = new Patient(request.FirstName, request.LastName, request.NationalId, request.DateOfBirth,
            request.Gender, request.BloodType, request.PhoneNumber, request.EmailAddress, request.Address,
            request.Allergies, request.EmergencyContactName, request.EmergencyContactPhone);
        await _patientRepository.AddAsync(patient,cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return patient.Id;
    }
}