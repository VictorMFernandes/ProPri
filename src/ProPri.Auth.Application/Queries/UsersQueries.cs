using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProPri.Users.Application.Queries.Dtos;
using ProPri.Users.Application.Queries.Filters;
using ProPri.Users.Domain;

namespace ProPri.Users.Application.Queries
{
    public class UsersQueries : IUsersQueries
    {
        private readonly IUserRepository _userRepository;

        public UsersQueries(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserIndexDto>> GetUsers(UserFilter filter)
        {
            var users = _userRepository.GetAllUsers().Select(u =>
                new UserIndexDto
                {
                    Name = u.Name.ToString(),
                    Role = _userManager.GetRolesAsync(u).Result.FirstOrDefault()
                });

            return users;
        }

        public async Task<UserIndexDto> GetUserById(Guid userId)
        {
            var users = context.Users
                .Where(x => x.Roles.Select(y => y.Id).Contains(roleId))
                .ToList();

            var user = await _userManager.FindByIdAsync(userId.ToString());
            var role = 
            var userDto = new UserFormDto
            {
                Id = user.Id,
                FirstName = user.Name.FirstName,
                Surname = user.Name.Surname,
                Email = user.Email,
                RoleId = 
            };

            return null;
        }
    }
}