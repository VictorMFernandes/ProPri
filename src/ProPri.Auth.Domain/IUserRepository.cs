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
        Task<UserFormDto> GetUserByIdWithUserRoles(Guid id);
        void UpdateUser(User user);

        #endregion

        #region Role

        Task<IEnumerable<RoleIdNameDto>> GetAllRoleIdName();

        #endregion

        #region Claim

        Task<bool> HasClaim(Guid userId, string claimValue);

        #endregion
    }
}