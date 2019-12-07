using Microsoft.AspNetCore.Identity;
using ProPri.Core.Constants;
using ProPri.Core.Domain.ValueObjects;
using ProPri.Users.Domain;

namespace ProPri.Users.Data
{
    public class UsersSeeder
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersSeeder(UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
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
            if (!_roleManager.RoleExistsAsync(ConstData.RoleManager).Result)
            {
                var role = new IdentityRole(ConstData.RoleManager);
                _roleManager.CreateAsync(role).Wait();
            }

            if (!_roleManager.RoleExistsAsync(ConstData.RolePed).Result)
            {
                var role = new IdentityRole(ConstData.RolePed);
                _roleManager.CreateAsync(role).Wait();
            }

            if (!_roleManager.RoleExistsAsync(ConstData.RoleFd).Result)
            {
                var role = new IdentityRole(ConstData.RoleFd);
                _roleManager.CreateAsync(role).Wait();
            }
        }

        private void SeedUsers()
        {
            if (_userManager.FindByEmailAsync(ConstData.OwnerEmail).Result == null)
            {
                var name = new PersonName(ConstData.OwnerFirstName, ConstData.OwnerSurname);
                var user = new User(name, ConstData.OwnerEmail);
                _userManager.CreateAsync(user, ConstData.OwnerPassword).Wait();
                _userManager.AddToRoleAsync(user, ConstData.RoleManager).Wait();
            }
        }
    }
}