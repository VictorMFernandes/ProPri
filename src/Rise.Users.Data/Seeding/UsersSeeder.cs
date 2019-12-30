using Rise.Core.Constants;
using Rise.Users.Domain;
using System;
using System.Threading.Tasks;

namespace Rise.Users.Data.Seeding
{
    public class UsersSeeder
    {
        private readonly IUserRepository _userRepo;
        private readonly UserSeedingOptions _userSeedingOpt;

        public UsersSeeder(IUserRepository userRepo, UserSeedingOptions userSeedingOpt)
        {
            _userRepo = userRepo;
            _userSeedingOpt = userSeedingOpt;
        }


        public async Task Seed()
        {
            await SeedRoles();
            await SeedUsers();
        }

        private async Task SeedRoles()
        {
            var rolesChanged = false;

            if (!await _userRepo.RoleExists(ConstData.RoleAdministrator))
            {
                var role = new Role(ConstData.RoleAdministrator);

                AddFdClaims(role);
                AddPedClaims(role);
                AddManagerClaims(role);
                AddAdminClaims(role);

                _userRepo.CreateRole(role);
                rolesChanged = true;
            }

            if (!await _userRepo.RoleExists(ConstData.RoleManager))
            {
                var role = new Role(ConstData.RoleManager);

                AddFdClaims(role);
                AddPedClaims(role);
                AddManagerClaims(role);

                _userRepo.CreateRole(role);
                rolesChanged = true;
            }

            if (!await _userRepo.RoleExists(ConstData.RolePed))
            {
                var role = new Role(ConstData.RolePed);

                AddFdClaims(role);
                AddPedClaims(role);

                _userRepo.CreateRole(role);
                rolesChanged = true;
            }

            if (!await _userRepo.RoleExists(ConstData.RoleFd))
            {
                var role = new Role(ConstData.RoleFd);

                AddFdClaims(role);

                _userRepo.CreateRole(role);
                rolesChanged = true;
            }

            if (rolesChanged) await _userRepo.Commit();
        }

        private static void AddAdminClaims(Role role)
        {

        }

        private static void AddManagerClaims(Role role)
        {
        }

        private static void AddPedClaims(Role role)
        {
            role.AddClaim(ConstData.ClaimTypeAuthorization, ConstData.ClaimUsersRead);
            role.AddClaim(ConstData.ClaimTypeAuthorization, ConstData.ClaimUsersWrite);
        }

        private static void AddFdClaims(Role role)
        {
            role.AddClaim(ConstData.ClaimTypeAuthorization, ConstData.ClaimStudentsRead);
            role.AddClaim(ConstData.ClaimTypeAuthorization, ConstData.ClaimStudentsWrite);
        }

        private async Task SeedUsers()
        {
            var usersChanged = false;

            if (!await _userRepo.UserExistsByEmail(_userSeedingOpt.AdminEmail))
            {
                var adminRole = await _userRepo.GetRoleByName(ConstData.RoleAdministrator);
                var managerRole = await _userRepo.GetRoleByName(ConstData.RoleManager);
                var pedRole = await _userRepo.GetRoleByName(ConstData.RolePed);
                var fdRole = await _userRepo.GetRoleByName(ConstData.RoleFd);

                var admin = User.CreateAdministrator(_userSeedingOpt.AdminName, _userSeedingOpt.AdminEmail, adminRole);
                await _userRepo.CreateUser(admin, _userSeedingOpt.AdminPassword);

                // TODO remove test seed
                const string simplePass = "12345678";

                var user = admin.CreateUser("Manager Surname", "manager@email.com", true, DateTime.Now.AddYears(-30), managerRole);
                user.EmailConfirmed = true;
                await _userRepo.CreateUser(user, simplePass);
                user = admin.CreateUser("Ped Surname", "ped@email.com", true, DateTime.Now.AddYears(-28), pedRole);
                user.EmailConfirmed = true;
                await _userRepo.CreateUser(user, simplePass);
                user = admin.CreateUser("Fd Surname", "fd@email.com", true, DateTime.Now.AddYears(-20), fdRole);
                user.EmailConfirmed = true;
                await _userRepo.CreateUser(user, simplePass);

                user = admin.CreateUser("Test1 Surname", "1@email.com", true, DateTime.Now.AddYears(-25), managerRole);
                user.EmailConfirmed = true;
                await _userRepo.CreateUser(user, simplePass);
                user = admin.CreateUser("Test2 Surname", "2@email.com", true, DateTime.Now.AddYears(-16), pedRole);
                user.EmailConfirmed = true;
                await _userRepo.CreateUser(user, simplePass);
                user = admin.CreateUser("Test3 Surname", "3@email.com", false, DateTime.Now.AddYears(-18), fdRole);
                user.EmailConfirmed = true;
                await _userRepo.CreateUser(user, simplePass);
                user = admin.CreateUser("Test4 Surname", "4@email.com", true, DateTime.Now.AddYears(-50), fdRole);
                user.EmailConfirmed = true;
                await _userRepo.CreateUser(user, simplePass);
                user = admin.CreateUser("Test5 Surname", "5@email.com", true, DateTime.Now.AddYears(-14), managerRole);
                user.EmailConfirmed = true;
                await _userRepo.CreateUser(user, simplePass);
                user = admin.CreateUser("Test6 Surname", "6@email.com", true, DateTime.Now.AddYears(-33), fdRole);
                user.EmailConfirmed = true;
                await _userRepo.CreateUser(user, simplePass);
                user = admin.CreateUser("Test7 Surname", "7@email.com", true, DateTime.Now.AddYears(-31), fdRole);
                user.EmailConfirmed = true;
                await _userRepo.CreateUser(user, simplePass);
                user = admin.CreateUser("Test8 Surname", "8@email.com", true, DateTime.Now.AddYears(-32), fdRole);
                user.EmailConfirmed = true;
                await _userRepo.CreateUser(user, simplePass);
                user = admin.CreateUser("Test9 Surname", "9@email.com", true, DateTime.Now.AddYears(-75), pedRole);
                user.EmailConfirmed = true;
                await _userRepo.CreateUser(user, simplePass);
                user = admin.CreateUser("Test10 Surname", "10@email.com", true, DateTime.Now.AddYears(-45), pedRole);
                user.EmailConfirmed = true;
                await _userRepo.CreateUser(user, simplePass);
                user = admin.CreateUser("Test11 Surname", "11@email.com", true, DateTime.Now.AddYears(-27), fdRole);
                user.EmailConfirmed = true;
                await _userRepo.CreateUser(user, simplePass);
                user = admin.CreateUser("Test12 Surname", "12@email.com", true, DateTime.Now.AddYears(-27), managerRole);
                user.EmailConfirmed = true;
                await _userRepo.CreateUser(user, simplePass);
                user = admin.CreateUser("Test13 Surname", "13@email.com", false, DateTime.Now.AddYears(-29), pedRole);
                user.EmailConfirmed = true;
                await _userRepo.CreateUser(user, simplePass);
                user = admin.CreateUser("Test14 Surname", "14@email.com", true, DateTime.Now.AddYears(-30), pedRole);
                user.EmailConfirmed = true;
                await _userRepo.CreateUser(user, simplePass);
                user = admin.CreateUser("Test15 Surname", "15@email.com", true, DateTime.Now.AddYears(-31), fdRole);
                user.EmailConfirmed = true;
                await _userRepo.CreateUser(user, simplePass);
                user = admin.CreateUser("Test16 Surname", "16@email.com", true, DateTime.Now.AddYears(-38), pedRole);
                user.EmailConfirmed = true;
                await _userRepo.CreateUser(user, simplePass);
                user = admin.CreateUser("Test17 Surname", "17@email.com", true, DateTime.Now.AddYears(-40), pedRole);
                user.EmailConfirmed = true;
                await _userRepo.CreateUser(user, simplePass);
                user = admin.CreateUser("Test18 Surname", "18@email.com", true, DateTime.Now.AddYears(-18), fdRole);
                user.EmailConfirmed = true;
                await _userRepo.CreateUser(user, simplePass);
                user = admin.CreateUser("Test19 Surname", "19@email.com", true, DateTime.Now.AddYears(-19), pedRole);
                user.EmailConfirmed = true;
                await _userRepo.CreateUser(user, simplePass);
                user = admin.CreateUser("Test20 Surname", "20@email.com", true, DateTime.Now.AddYears(-20), managerRole);
                user.EmailConfirmed = true;
                await _userRepo.CreateUser(user, simplePass);

                usersChanged = true;
            }

            if (usersChanged) await _userRepo.Commit();
        }
    }
}