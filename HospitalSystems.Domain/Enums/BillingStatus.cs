namespace HospitalSystems.Domain.Enums;

public enum BillingStatus
{
    Pending = 0,
    Paid = 1,
    PartiallyPaid = 2,
    Cancelled = 3,
    Refunded = 4
}