using Microsoft.AspNetCore.Identity;
using ProPri.Core.Helpers;
using ProPri.Users.Application.Queries.Filters;
using ProPri.Users.Domain;
using ProPri.Users.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ProPri.Core.Constants;

namespace ProPri.Users.Application.Queries
{
    public class UsersQueries : IUsersQueries
    {
        private readonly IUserRepository _userRepository;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        #region Constructors

        public UsersQueries(IUserRepository userRepository,
                            SignInManager<User> signInManager,
                            UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        #endregion

        #region User

        public async Task<PaginatedList<UserIndexDto>> GetUsers(UserFilter filter)
        {
            var users = await _userRepository.GetUsers(filter.PageNumber, filter.PageSize);

            return users;
        }

        public async Task<UserFormDto> GetUserById(Guid userId)
        {
            return await _userRepository.GetUserById(userId);
        }

        public bool IsSignedIn(ClaimsPrincipal user)
        {
            return _signInManager.IsSignedIn(user);
        }

        #endregion

        #region Role

        public async Task<IEnumerable<RoleIdNameDto>> GetAllRoleIdName()
        {
            return await _userRepository.GetAllRoleIdName();
        }

        #endregion

        #region Claim

        public bool IsAuthorized(ClaimsPrincipal user, string claim)
        {
            // Tem que pegar do banco, nesse http context ainda não tem a claim
            _userManager.Clai
            var result = user.HasClaim(c => c.Value == claim);
            return result;
        }

        #endregion
    }
}