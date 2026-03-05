using HospitalSystems.Domain.Common.Interfaces;

namespace HospitalSystems.Domain.Appointments;

public interface IAppointmentRepository : IRepository<Appointment>
{
    Task<List<Appointment>> GetByDoctorAndDateAsync(Guid doctorId, DateTime date, CancellationToken cancellationToken = default);
    Task<List<Appointment>> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default);
    Task<bool> HasConflictAsync(Guid doctorId, DateTime dateTime, CancellationToken cancellationToken = default);
}