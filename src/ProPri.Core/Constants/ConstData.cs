namespace ProPri.Core.Constants
{
    public class ConstData
    {
        #region Users

        public const string SimplePassword = "12345678";

        public const string Administrator = "admin@email.com";
        public const string AdministratorFirstName = "AdminFirstName";
        public const string AdministratorSurname = "AdminSurname";

        public const string Manager = "manager@email.com";
        public const string ManagerFirstName = "ManagerFirstName";
        public const string ManagerSurname = "ManagerSurname";

        public const string PedEmail = "ped@email.com";
        public const string PedFirstName = "PedFirstName";
        public const string PedSurname = "PedSurname";

        public const string FdEmail = "fd@email.com";
        public const string FdFirstName = "FdFirstName";
        public const string FdSurname = "FdSurname";

        #endregion

        #region Roles

        public const string RoleAdministrator = "Administrator";
        public const string RoleManager = "Manager";
        public const string RolePed = "PED";
        public const string RoleFd = "FD";

        #endregion

        #region Claim

        public const string ClaimTypeAuthorization = "AuthorizationClaim";

        public const string ClaimUsersRead = "UsersRead";
        public const string ClaimUsersWrite = "UsersWrite";

        public const string ClaimStudentsRead = "StudentsRead";
        public const string ClaimStudentsWrite = "StudentsWrite";

        #endregion
    }
}