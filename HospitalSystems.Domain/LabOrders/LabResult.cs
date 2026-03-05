using HospitalSystems.Domain.Common;

namespace HospitalSystems.Domain.LabOrders;

public class LabResult:AuditableEntity
{
    public string ResultData { get; private set; }
    public string Notes { get; private set; }
    public string? FilePath { get; private set; }
    public Guid LabOrderId { get; private set; }
    private LabResult(){}

    public LabResult(string resultData, string notes, string filePath, Guid labOrderId)
    {
        ResultData = resultData;
        Notes = notes;
        LabOrderId = labOrderId;
        FilePath = filePath;
    }
}