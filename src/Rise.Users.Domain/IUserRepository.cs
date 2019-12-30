using Microsoft.AspNetCore.Identity;
using Rise.Core.Data;
using Rise.Core.Helpers;
using Rise.Users.Domain.Dtos;
using Rise.Users.Domain.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rise.Users.Domain
{
    public interface IUserRepository : IRepository<User>
    {
        #region User

        Task<PaginatedList<UserIndexDto>> GetUsers(UserFilter filter);
        Task<User> GetUserById(Guid id);
        Task<User> GetUserByEmail(string email);
        Task<UserFormDto> GetUserFormById(Guid id);
        Task<bool> UserExistsByEmail(string email);
        Task<IdentityResult> CreateUser(User user, string tempPassword);
        void UpdateUser(User user);

        #endregion

        #region Auth

        Task<SignInResult> SignIn(string email, string password);
        Task SignOut();
        Task<IdentityResult> ChangePassword(User user, string currentPassword, string newPassword);

        #endregion

        #region Role

        Task<IEnumerable<RoleIdNameDto>> GetAllRoleIdName();
        Task<Role> GetRoleById(Guid id);
        Task<Role> GetRoleByName(string roleName);
        Task<bool> RoleExists(string roleName);
        void CreateRole(Role role);

        #endregion

        #region Claim

        Task<bool> HasClaim(Guid userId, string claimValue);

        #endregion
    }
}