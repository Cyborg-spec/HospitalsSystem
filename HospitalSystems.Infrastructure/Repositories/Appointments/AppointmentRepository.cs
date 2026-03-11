using HospitalSystems.Domain.Appointments;
using HospitalSystems.Domain.Enums;
using HospitalSystems.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystems.Infrastructure.Repositories.Appointments;

public class AppointmentRepository(ApplicationDbContext dbContext)
    : BaseRepository<Appointment>(dbContext), IAppointmentRepository
{
    public async Task<IReadOnlyList<Appointment>> GetByDoctorAndDateAsync(Guid doctorId, DateTime date, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Where(a => a.DoctorId == doctorId && a.StartTime.Date == date.Date)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Appointment>> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Where(a => a.PatientId == patientId)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> HasConflictAsync(Guid doctorId, DateTime startTime, DateTime endTime, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AnyAsync(a => a.DoctorId == doctorId
                        && a.StartTime < endTime
                        && a.EndTime > startTime
                        && a.Status != AppointmentStatus.Cancelled,
                cancellationToken);
    }
}