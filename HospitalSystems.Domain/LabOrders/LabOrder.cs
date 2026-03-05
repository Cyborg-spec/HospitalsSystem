using HospitalSystems.Domain.Common;
using HospitalSystems.Domain.Enums;
using HospitalSystems.Domain.MedicalRecords;
using HospitalSystems.Domain.Users;

namespace HospitalSystems.Domain.LabOrders;

public class LabOrder : AuditableEntity
{
    public string TestType { get; private set; }
    public LabOrderStatus LabOrderStatus { get; private set; }
    public Guid DoctorId { get; private set; }
    public Guid MedicalRecordId { get; private set; }

    public Doctor Doctor { get; private set; }

    private LabOrder()
    {
    }

    public LabOrder(string testType, Guid doctorId, Guid medicalRecordId)
    {
        TestType = testType;
        LabOrderStatus = LabOrderStatus.Pending;
        DoctorId = doctorId;
        MedicalRecordId = medicalRecordId;
    }

}