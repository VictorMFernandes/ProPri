using Microsoft.AspNetCore.Identity;
using ProPri.Core.Data;
using ProPri.Core.Helpers;
using ProPri.Users.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProPri.Users.Domain
{
    public interface IUserRepository : IRepository<User>
    {
        #region User

        Task<PaginatedList<UserIndexDto>> GetUsers(int pageNumber, int pageSize);
        Task<User> GetUserById(Guid id);
        Task<User> GetUserByEmail(string email);
        Task<UserFormDto> GetUserFormById(Guid id);
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

        #endregion

        #region Claim

        Task<bool> HasClaim(Guid userId, string claimValue);

        #endregion
    }
}