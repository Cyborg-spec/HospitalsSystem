using HospitalSystems.Domain.Common.Interfaces;
using HospitalSystems.Domain.Enums;

namespace HospitalSystems.Domain.LabOrders;

public interface ILabOrderRepository : IRepository<LabOrder>
{
    Task<IReadOnlyList<LabOrder>> GetByMedicalRecordIdAsync(Guid medicalRecordId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<LabOrder>> GetByDoctorIdAsync(Guid doctorId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<LabOrder>> GetByStatusAsync(LabOrderStatus status, CancellationToken cancellationToken = default);
}