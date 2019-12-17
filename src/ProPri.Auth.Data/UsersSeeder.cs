using Microsoft.AspNetCore.Identity;
using ProPri.Core.Constants;
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
                var user = new User($"{ConstData.AdministratorFirstName} {ConstData.AdministratorSurname}", ConstData.Administrator, true);
                _userManager.CreateAsync(user, ConstData.SimplePassword).Wait();
                _userManager.AddToRoleAsync(user, ConstData.RoleManager).Wait();
            }

            if (_userManager.FindByEmailAsync(ConstData.Manager).Result == null)
            {
                var user = new User($"{ConstData.ManagerFirstName} {ConstData.ManagerSurname}", ConstData.Manager, false);
                _userManager.CreateAsync(user, ConstData.SimplePassword).Wait();
                _userManager.AddToRoleAsync(user, ConstData.RoleManager).Wait();
            }

            if (_userManager.FindByEmailAsync(ConstData.PedEmail).Result == null)
            {
                var user = new User($"{ConstData.PedFirstName} {ConstData.PedSurname}", ConstData.PedEmail, false);
                _userManager.CreateAsync(user, ConstData.SimplePassword).Wait();
                _userManager.AddToRoleAsync(user, ConstData.RolePed).Wait();
            }

            if (_userManager.FindByEmailAsync(ConstData.FdEmail).Result == null)
            {
                var user = new User($"{ConstData.FdFirstName} {ConstData.FdSurname}", ConstData.FdEmail, false);
                _userManager.CreateAsync(user, ConstData.SimplePassword).Wait();
                _userManager.AddToRoleAsync(user, ConstData.RoleFd).Wait();
            }

            if (_userManager.FindByEmailAsync("teste@email.com").Result == null)
            {
                var user = new User("Teste TesteSur", "teste@email.com", false);
                _userManager.CreateAsync(user, ConstData.SimplePassword).Wait();
                _userManager.AddToRoleAsync(user, ConstData.RoleFd).Wait();
            }

            if (_userManager.FindByEmailAsync("teste1@email.com").Result == null)
            {
                var user = new User("Teste1 Teste1Sur", "teste1@email.com", false);
                _userManager.CreateAsync(user, ConstData.SimplePassword).Wait();
                _userManager.AddToRoleAsync(user, ConstData.RoleFd).Wait();
            }

            if (_userManager.FindByEmailAsync("teste2@email.com").Result == null)
            {
                var user = new User("Teste2 Teste2Sur", "teste2@email.com", false);
                _userManager.CreateAsync(user, ConstData.SimplePassword).Wait();
                _userManager.AddToRoleAsync(user, ConstData.RoleFd).Wait();
            }

            if (_userManager.FindByEmailAsync("teste3@email.com").Result == null)
            {
                var user = new User("Teste3 Teste3Sur", "teste3@email.com", false);
                _userManager.CreateAsync(user, ConstData.SimplePassword).Wait();
                _userManager.AddToRoleAsync(user, ConstData.RoleFd).Wait();
            }

            if (_userManager.FindByEmailAsync("teste4@email.com").Result == null)
            {
                var user = new User("Teste4 TesteSur4", "teste4@email.com", false);
                _userManager.CreateAsync(user, ConstData.SimplePassword).Wait();
                _userManager.AddToRoleAsync(user, ConstData.RoleFd).Wait();
            }

            if (_userManager.FindByEmailAsync("teste5@email.com").Result == null)
            {
                var user = new User("Teste5 Teste5Sur", "teste5@email.com", false);
                _userManager.CreateAsync(user, ConstData.SimplePassword).Wait();
                _userManager.AddToRoleAsync(user, ConstData.RoleFd).Wait();
            }

            if (_userManager.FindByEmailAsync("teste6@email.com").Result == null)
            {
                var user = new User("Teste6 Teste6Sur", "teste6@email.com", false);
                _userManager.CreateAsync(user, ConstData.SimplePassword).Wait();
                _userManager.AddToRoleAsync(user, ConstData.RoleFd).Wait();
            }

            if (_userManager.FindByEmailAsync("teste7@email.com").Result == null)
            {
                var user = new User("Teste7 Teste7Sur", "teste7@email.com", false);
                _userManager.CreateAsync(user, ConstData.SimplePassword).Wait();
                _userManager.AddToRoleAsync(user, ConstData.RoleFd).Wait();
            }

            if (_userManager.FindByEmailAsync("teste8@email.com").Result == null)
            {
                var user = new User("Teste8 Teste8Sur", "teste8@email.com", false);
                _userManager.CreateAsync(user, ConstData.SimplePassword).Wait();
                _userManager.AddToRoleAsync(user, ConstData.RoleFd).Wait();
            }
        }
    }
}