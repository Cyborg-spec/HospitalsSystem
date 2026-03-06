using System.Linq.Expressions;
using HospitalSystems.Domain.Appointments;
using HospitalSystems.Domain.Enums;
using HospitalSystems.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystems.Infrastructure.Repositories.Appointments;

public class AppointmentRepository:IAppointmentRepository
{
    private readonly ApplicationDbContext _dbContext;

    public AppointmentRepository(ApplicationDbContext context)
    {
        _dbContext = context;
    }
    public async Task<Appointment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var appointment = await _dbContext.Appointments.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        return appointment;
    }

    public async Task<List<Appointment>> GetAllAsync(CancellationToken cancellationToken = default)=>
    await  _dbContext.Appointments.ToListAsync(cancellationToken);

    public async Task<List<Appointment>> FindAsync(Expression<Func<Appointment, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Appointments.Where(predicate).ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Appointments.AnyAsync(a => a.Id == id, cancellationToken);
    }

    public async Task AddAsync(Appointment entity, CancellationToken cancellationToken = default)
    { 
        await _dbContext.Appointments.AddAsync(entity, cancellationToken);
    }

    public void Update(Appointment entity)
    {
        _dbContext.Appointments.Update(entity);
    }

    public void Delete(Appointment entity)
    {
        _dbContext.Appointments.Remove(entity);
    }

    public async Task<List<Appointment>> GetByDoctorAndDateAsync(Guid doctorId, DateTime date, CancellationToken cancellationToken = default)
    {
        var appointments = await _dbContext.Appointments
            .Where(a => a.DoctorId == doctorId && a.DateTime.Date == date.Date)
            .ToListAsync(cancellationToken);
        return appointments;
    }

    public async Task<List<Appointment>> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken = default)
    {
        var appointments = await _dbContext.Appointments
            .Where(a => a.PatientId == patientId)
            .ToListAsync(cancellationToken);
        return appointments;
    }

    public async Task<bool> HasConflictAsync(Guid doctorId, DateTime dateTime, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Appointments
            .AnyAsync(a => a.DoctorId == doctorId
                        && a.DateTime == dateTime
                        && a.Status != AppointmentStatus.Cancelled,
                cancellationToken);
    }
}