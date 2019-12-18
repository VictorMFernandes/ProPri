using Microsoft.AspNetCore.Identity;
using Rise.Core.Helpers;
using Rise.Users.Domain;
using Rise.Users.Domain.Dtos;
using Rise.Users.Domain.Filters;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Rise.Users.Application.Queries
{
    public class UsersQueries : IUsersQueries
    {
        private readonly IUserRepository _userRepository;
        private readonly SignInManager<User> _signInManager;

        #region Constructors

        public UsersQueries(IUserRepository userRepository,
                            SignInManager<User> signInManager)
        {
            _userRepository = userRepository;
            _signInManager = signInManager;
        }

        #endregion

        #region User

        public async Task<PaginatedList<UserIndexDto>> GetUsers(UserFilter filter)
        {
            var users = await _userRepository.GetUsers(filter);

            return users;
        }

        public async Task<UserFormDto> GetUserById(Guid userId)
        {
            return await _userRepository.GetUserFormById(userId);
        }

        public bool IsSignedIn(ClaimsPrincipal user)
        {
            return _signInManager.IsSignedIn(user);
        }

        #endregion

        #region ActiveUserWithRoleExists

        public async Task<IEnumerable<RoleIdNameDto>> GetAllRoleIdName()
        {
            return await _userRepository.GetAllRoleIdName();
        }

        #endregion

        #region Claim

        public async Task<bool> IsAuthorized(Guid userId, string claimValue)
        {
            return await _userRepository.HasClaim(userId, claimValue);
        }

        #endregion
    }
}