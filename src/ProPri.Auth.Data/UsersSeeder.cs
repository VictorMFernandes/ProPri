using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using ProPri.Core.Constants;
using ProPri.Core.Domain.ValueObjects;
using ProPri.Users.Domain;

namespace ProPri.Users.Data
{
    public class UsersSeeder
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        private readonly IUserRepository _userRepository;

        public UsersSeeder(UserManager<User> userManager,
                           RoleManager<Role> roleManager,
                           IUserRepository userRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userRepository = userRepository;
        }

        public void Seed()
        {
            SeedRoles();
            SeedUsers();
        }

        private void SeedRoles()
        {
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
            if (_userManager.FindByEmailAsync(ConstData.OwnerEmail).Result == null)
            {
                var name = new PersonName(ConstData.OwnerFirstName, ConstData.OwnerSurname);
                var user = new User(name, ConstData.OwnerEmail);
                _userManager.CreateAsync(user, ConstData.SimplePassword).Wait();
                _userManager.AddToRoleAsync(user, ConstData.RoleManager).Wait();
            }

            if (_userManager.FindByEmailAsync(ConstData.PedEmail).Result == null)
            {
                var name = new PersonName(ConstData.PedFirstName, ConstData.PedSurname);
                var user = new User(name, ConstData.PedEmail);
                _userManager.CreateAsync(user, ConstData.SimplePassword).Wait();
                _userManager.AddToRoleAsync(user, ConstData.RolePed).Wait();
            }

            if (_userManager.FindByEmailAsync(ConstData.FdEmail).Result == null)
            {
                var name = new PersonName(ConstData.FdFirstName, ConstData.FdSurname);
                var user = new User(name, ConstData.FdEmail);
                _userManager.CreateAsync(user, ConstData.SimplePassword).Wait();
                _userManager.AddToRoleAsync(user, ConstData.RoleFd).Wait();
            }
        }
    }
}