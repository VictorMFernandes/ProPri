using ProPri.Core.Helpers;
using ProPri.Users.Application.Queries.Filters;
using ProPri.Users.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProPri.Users.Application.Queries
{
    public interface IUsersQueries
    {
        #region User

        Task<PaginatedList<UserIndexDto>> GetUsers(UserFilter filter);
        Task<UserFormDto> GetUserById(Guid userId);

        #endregion

        #region Role

        Task<IEnumerable<RoleIdNameDto>> GetAllRoleIdName();

        #endregion
    }
}