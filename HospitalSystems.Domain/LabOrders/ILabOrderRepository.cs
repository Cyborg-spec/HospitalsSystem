using HospitalSystems.Domain.Common.Interfaces;
using HospitalSystems.Domain.Enums;

namespace HospitalSystems.Domain.LabOrders;

public interface ILabOrderRepository : IRepository<LabOrder>
{
    Task<List<LabOrder>> GetByMedicalRecordIdAsync(Guid medicalRecordId, CancellationToken cancellationToken = default);
    Task<List<LabOrder>> GetByDoctorIdAsync(Guid doctorId, CancellationToken cancellationToken = default);
    Task<List<LabOrder>> GetByStatusAsync(LabOrderStatus status, CancellationToken cancellationToken = default);
}