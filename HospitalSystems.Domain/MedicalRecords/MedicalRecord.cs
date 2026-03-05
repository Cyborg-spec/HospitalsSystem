using HospitalSystems.Domain.Common;

namespace HospitalSystems.Domain.MedicalRecords;

public class MedicalRecord:AuditableEntity
{
    public string ChiefComplaint { get; private set; }
    public string Symptoms { get; private set; }
    public string Diagnosis { get; private set; }
    public string DiagnosisCode { get; private set; }
    public string Vitals{ get; private set; }
    public string Notes { get; private set; }
    public DateTime? FollowUpDate { get; private set; }
    public Guid PatientId { get; private set; }
    public Guid DoctorId { get; private set; }
    public Guid AppointmentId { get; private set; }
    private MedicalRecord(){}

    public MedicalRecord(string chiefComplaint, string symptoms, string diagnosis, string diagnosisCode, string vitals,
        string notes,
        DateTime followUpDate, Guid patientId, Guid doctorId, Guid appointmentId)
    {
        ChiefComplaint = chiefComplaint;
        Symptoms = symptoms;
        Diagnosis = diagnosis;
        DiagnosisCode = diagnosisCode;
        Vitals = vitals;
        Notes = notes;
        PatientId = patientId;
        DoctorId = doctorId;
        AppointmentId = appointmentId;
        FollowUpDate = followUpDate;
    }
}