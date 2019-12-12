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
        Task<UserFormDto> GetUserFormById(Guid id);
        void UpdateUser(User user);
        Task<User> GetUserById(Guid id);
        Task<int> QtyOfActiveUsersInRole(string roleName);

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