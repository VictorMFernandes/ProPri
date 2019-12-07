using ProPri.Users.Application.Queries.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProPri.Users.Application.Queries
{
    public interface IUsersQueries
    {
        Task<IEnumerable<UserIndexDto>> GetUsers();
    }
}