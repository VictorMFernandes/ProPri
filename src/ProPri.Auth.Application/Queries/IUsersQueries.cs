using ProPri.Users.Application.Queries.Dtos;
using ProPri.Users.Application.Queries.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProPri.Users.Application.Queries
{
    public interface IUsersQueries
    {
        Task<IEnumerable<UserIndexDto>> GetUsers(UserFilter filter);
        Task<UserIndexDto> GetUserById(Guid userId);
    }
}