using System.Collections.Immutable;
using HospitalSystems.Domain.Common.Interfaces;

namespace HospitalSystems.Domain.Appointments;

public interface IAppointmentRepository : IRepository<Appointment>
{
    Task<IImmutableList<Appointment>> GetByDoctorAndDateAsync(Guid doctorId, DateTime date, CancellationToken cancellationToken = default);
    Task<IImmutableList<Appointment>> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default);
    Task<bool> HasConflictAsync(Guid doctorId, DateTime startTime, DateTime endTime, CancellationToken cancellationToken = default);
}