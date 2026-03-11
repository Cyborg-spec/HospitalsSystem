using HospitalSystems.Domain.Appointments;
using HospitalSystems.Domain.AuditLogs;
using HospitalSystems.Domain.Billing;
using HospitalSystems.Domain.Common.Interfaces;
using HospitalSystems.Domain.Hospitals;
using HospitalSystems.Domain.LabOrders;
using HospitalSystems.Domain.MedicalRecords;
using HospitalSystems.Domain.Patients;
using HospitalSystems.Domain.Prescriptions;
using HospitalSystems.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystems.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<User, IdentityRole<Guid>, Guid>(options),IUnitOfWork
{

    // Organization
    public DbSet<Hospital> Hospitals => Set<Hospital>();
    public DbSet<Department> Departments => Set<Department>();

    // Staff
    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<Nurse> Nurses => Set<Nurse>();
    public DbSet<Receptionist> Receptionists => Set<Receptionist>();
    public DbSet<LabTechnician> LabTechnicians => Set<LabTechnician>();
    public DbSet<Pharmacist> Pharmacists => Set<Pharmacist>();

    // Patients
    public DbSet<Patient> Patients => Set<Patient>();

    // Clinical
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<MedicalRecord> MedicalRecords => Set<MedicalRecord>();
    public DbSet<Prescription> Prescriptions => Set<Prescription>();
    public DbSet<PrescriptionItem> PrescriptionItems => Set<PrescriptionItem>();
    public DbSet<LabOrder> LabOrders => Set<LabOrder>();
    public DbSet<LabResult> LabResults => Set<LabResult>();

    // Billing
    public DbSet<Invoice> Invoices => Set<Invoice>();

    // Audit
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}