using HospitalSystems.Domain.MedicalRecords;
using HospitalSystems.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystems.Infrastructure.Repositories.MedicalRecords;

public class MedicalRecordRepository(ApplicationDbContext dbContext)
    : BaseRepository<MedicalRecord>(dbContext), IMedicalRecordRepository
{
    public async Task<IReadOnlyList<MedicalRecord>> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default)
    {
        return await DbSet.Where(m => m.PatientId == patientId).ToListAsync(cancellationToken);
    }

    public async Task<MedicalRecord?> GetByAppointmentIdAsync(Guid appointmentId, CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(m => m.AppointmentId == appointmentId, cancellationToken);
    }
}