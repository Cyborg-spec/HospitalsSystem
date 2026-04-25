using System.Collections.Immutable;
using HospitalSystems.Domain.MedicalRecords;
using HospitalSystems.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystems.Infrastructure.Repositories.MedicalRecords;

public class MedicalRecordRepository(ApplicationDbContext dbContext)
    : BaseRepository<MedicalRecord>(dbContext), IMedicalRecordRepository
{
    public async Task<IImmutableList<MedicalRecord>> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default)
    {
        return (await DbSet.Where(m => m.PatientId == patientId).ToListAsync(cancellationToken)).ToImmutableList();
    }

    public async Task<MedicalRecord?> GetByAppointmentIdAsync(Guid appointmentId, CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(m => m.AppointmentId == appointmentId, cancellationToken);
    }
}