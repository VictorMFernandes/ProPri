using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ProPri.Users.Application.Queries.Dtos;
using ProPri.Users.Domain;

namespace ProPri.Users.Application.Queries
{
    public class UsersQueries : IUsersQueries
    {
        private readonly UserManager<User> _userManager;

        public UsersQueries(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<UserIndexDto>> GetUsers()
        {
            var users = _userManager.Users.Select(u =>
                new UserIndexDto
                {
                    Name = u.Name.ToString(),
                    Role = _userManager.GetRolesAsync(u).Result.FirstOrDefault()
                });

            return users;
        }
    }
}