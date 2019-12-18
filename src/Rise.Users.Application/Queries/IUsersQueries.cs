using Rise.Core.Helpers;
using Rise.Users.Domain.Dtos;
using Rise.Users.Domain.Filters;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Rise.Users.Application.Queries
{
    public interface IUsersQueries
    {
        #region User

        Task<PaginatedList<UserIndexDto>> GetUsers(UserFilter filter);
        Task<UserFormDto> GetUserById(Guid userId);
        bool IsSignedIn(ClaimsPrincipal user);

        #endregion

        #region ActiveUserWithRoleExists

        Task<IEnumerable<RoleIdNameDto>> GetAllRoleIdName();

        #endregion

        #region Claim

        Task<bool> IsAuthorized(Guid userId, string claimValue);

        #endregion
    }
}