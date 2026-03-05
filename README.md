# 🏥 Hospital Records System

A digital hospital records management system designed to replace paper-based record keeping in governmental hospitals in Azerbaijan.

## Problem

In Azerbaijan, governmental hospitals still manage patient records, appointments, prescriptions, and lab orders by hand. This leads to:
- Lost or damaged records
- Illegible handwriting
- Delayed lab results
- No audit trail for changes
- Difficulty tracking patient history across visits

## Solution

A REST API that digitalizes the entire hospital workflow — from patient registration to billing — with role-based access control ensuring each staff member only sees what they're authorized to access.

## Tech Stack

| Layer | Technology |
|---|---|
| **Language** | C# / .NET 10 |
| **API** | ASP.NET Core Web API |
| **ORM** | Entity Framework Core |
| **Auth** | ASP.NET Identity + JWT |
| **Database** | PostgreSQL |
| **Architecture** | Vertical Slice + CQRS (MediatR) |
| **Validation** | FluentValidation |

## Project Structure

```
HospitalSystems/
├── HospitalSystems.Domain/            ← Entities, Enums, Repository Interfaces
│   ├── Common/                        ← BaseEntity, AuditableEntity, IRepository, IUnitOfWork
│   ├── Enums/                         ← UserRole, AppointmentStatus, Gender, BloodType...
│   ├── Hospitals/                     ← Hospital, Department entities + repos
│   ├── Users/                         ← User (Identity), Doctor, Nurse, Pharmacist...
│   ├── Patients/                      ← Patient entity + repo
│   ├── Appointments/                  ← Appointment entity + repo
│   ├── MedicalRecords/                ← MedicalRecord entity + repo
│   ├── Prescriptions/                 ← Prescription, PrescriptionItem + repo
│   ├── LabOrders/                     ← LabOrder, LabResult + repo
│   ├── Billing/                       ← Invoice entity + repo
│   └── AuditLogs/                     ← AuditLog entity
│
├── HospitalSystems.Infrastructure/    ← EF Core, Repositories, Identity, JWT
│   ├── Persistence/
│   │   ├── ApplicationDbContext.cs
│   │   └── Configurations/            ← EF Core entity configurations
│   └── Repositories/                  ← Repository implementations
│
├── HospitalSystems.Application/       ← MediatR handlers, validators, DTOs
│
└── HospitalSystems.Presentation/      ← Controllers, Middleware, Program.cs
```

## Roles & Access

| Role | Permissions |
|---|---|
| **SuperAdmin** | Full system access, manage hospitals and staff |
| **HospitalAdmin** | Manage their hospital's departments and staff |
| **Doctor** | Create medical records, order labs, write prescriptions |
| **Nurse** | View patient info, update vitals |
| **Receptionist** | Register patients, schedule appointments, create invoices |
| **LabTechnician** | View lab orders, upload results |
| **Pharmacist** | View and dispense prescriptions |

## Key Features

- **Patient Management** — Registration with national ID, medical history
- **Appointment Scheduling** — Conflict detection, doctor availability
- **Medical Records** — Diagnosis, symptoms, vitals, follow-up dates
- **Prescriptions** — Multi-item prescriptions with pharmacy dispensing workflow
- **Lab Orders** — Order → In Progress → Result upload pipeline
- **Billing** — Invoice generation per appointment
- **Audit Logging** — Tracks every data change (who, what, when)
- **Soft Deletes** — Records are never permanently deleted

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/)

### Run

```bash
# Clone
git clone https://github.com/Cyborg-spec/HospitalSystems.git
cd HospitalSystems

# Update connection string in appsettings.json

# Run migrations
dotnet ef database update --project HospitalSystems.Infrastructure --startup-project HospitalSystems.Presentation

# Start the API
dotnet run --project HospitalSystems.Presentation
```

The API will be available at `https://localhost:5001` with Swagger UI.

## License

This project is for educational purposes.
