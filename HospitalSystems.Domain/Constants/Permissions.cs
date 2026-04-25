namespace HospitalSystems.Domain.Constants;

public static class Permissions
{
    public static class Patients
    {
        public const string View = "Permissions.Patients.View";
        public const string Edit = "Permissions.Patients.Edit";
        public const string Delete = "Permissions.Patients.Delete";
        public const string Create = "Permissions.Patients.Create";
    }

    public static class Users
    {
        public const string Manage = "Permissions.Users.Manage";
    }

}