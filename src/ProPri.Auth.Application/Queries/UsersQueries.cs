using ProPri.Core.Helpers;
using ProPri.Users.Application.Queries.Filters;
using ProPri.Users.Domain;
using ProPri.Users.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ProPri.Users.Application.Queries
{
    public class UsersQueries : IUsersQueries
    {
        private readonly IUserRepository _userRepository;

        public UsersQueries(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<PaginatedList<UserIndexDto>> GetUsers(UserFilter filter)
        {
            var users = await _userRepository.GetUsers(filter.PageNumber, filter.PageSize);

            return users;
        }

        public async Task<UserFormDto> GetUserById(Guid userId)
        {
            return await _userRepository.GetUserById(userId);
        }

        public async Task<IEnumerable<RoleIdNameDto>> GetAllRoleIdName()
        {
            return await _userRepository.GetAllRoleIdName();
        }
    }
}