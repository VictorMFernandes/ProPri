using Microsoft.AspNetCore.Identity;
using ProPri.Core.Constants;
using ProPri.Core.Domain.ValueObjects;
using ProPri.Users.Domain;
using System.Security.Claims;

namespace ProPri.Users.Data
{
    public class UsersSeeder
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public UsersSeeder(UserManager<User> userManager,
                           RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Seed()
        {
            SeedRoles();
            SeedUsers();
        }

        private void SeedRoles()
        {
            if (!_roleManager.RoleExistsAsync(ConstData.RoleAdministrator).Result)
            {
                var role = new Role(ConstData.RoleAdministrator);
                _roleManager.CreateAsync(role).Wait();

                AddFdClaims(role);
                AddPedClaims(role);
                AddManagerClaims(role);
                AddAdminClaims(role);
            }

            if (!_roleManager.RoleExistsAsync(ConstData.RoleManager).Result)
            {
                var role = new Role(ConstData.RoleManager);
                _roleManager.CreateAsync(role).Wait();

                AddFdClaims(role);
                AddPedClaims(role);
                AddManagerClaims(role);
            }

            if (!_roleManager.RoleExistsAsync(ConstData.RolePed).Result)
            {
                var role = new Role(ConstData.RolePed);
                _roleManager.CreateAsync(role).Wait();

                AddFdClaims(role);
                AddPedClaims(role);
            }

            if (!_roleManager.RoleExistsAsync(ConstData.RoleFd).Result)
            {
                var role = new Role(ConstData.RoleFd);
                _roleManager.CreateAsync(role).Wait();

                AddFdClaims(role);
            }
        }

        private void AddAdminClaims(Role role)
        {

        }

        private void AddManagerClaims(Role role)
        {
        }

        private void AddPedClaims(Role role)
        {
            _roleManager.AddClaimAsync(role, new Claim(ConstData.ClaimTypeAuthorization, ConstData.ClaimUsersRead));
            _roleManager.AddClaimAsync(role, new Claim(ConstData.ClaimTypeAuthorization, ConstData.ClaimUsersWrite));
        }

        private void AddFdClaims(Role role)
        {
            _roleManager.AddClaimAsync(role, new Claim(ConstData.ClaimTypeAuthorization, ConstData.ClaimStudentsRead));
            _roleManager.AddClaimAsync(role, new Claim(ConstData.ClaimTypeAuthorization, ConstData.ClaimStudentsWrite));
        }

        private void SeedUsers()
        {
            if (_userManager.FindByEmailAsync(ConstData.Administrator).Result == null)
            {
                var name = new PersonName(ConstData.AdministratorFirstName, ConstData.AdministratorSurname);
                var user = new User(name, ConstData.Administrator, true);
                _userManager.CreateAsync(user, ConstData.SimplePassword).Wait();
                _userManager.AddToRoleAsync(user, ConstData.RoleManager).Wait();
            }

            if (_userManager.FindByEmailAsync(ConstData.Manager).Result == null)
            {
                var name = new PersonName(ConstData.ManagerFirstName, ConstData.ManagerSurname);
                var user = new User(name, ConstData.Manager, false);
                _userManager.CreateAsync(user, ConstData.SimplePassword).Wait();
                _userManager.AddToRoleAsync(user, ConstData.RoleManager).Wait();
            }

            if (_userManager.FindByEmailAsync(ConstData.PedEmail).Result == null)
            {
                var name = new PersonName(ConstData.PedFirstName, ConstData.PedSurname);
                var user = new User(name, ConstData.PedEmail, false);
                _userManager.CreateAsync(user, ConstData.SimplePassword).Wait();
                _userManager.AddToRoleAsync(user, ConstData.RolePed).Wait();
            }

            if (_userManager.FindByEmailAsync(ConstData.FdEmail).Result == null)
            {
                var name = new PersonName(ConstData.FdFirstName, ConstData.FdSurname);
                var user = new User(name, ConstData.FdEmail, false);
                _userManager.CreateAsync(user, ConstData.SimplePassword).Wait();
                _userManager.AddToRoleAsync(user, ConstData.RoleFd).Wait();
            }
        }
    }
}