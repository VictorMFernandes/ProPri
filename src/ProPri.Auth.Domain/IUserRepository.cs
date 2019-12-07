using ProPri.Core.Data;
using ProPri.Core.Helpers;
using ProPri.Users.Domain.Dtos;
using System;
using System.Threading.Tasks;

namespace ProPri.Users.Domain
{
    public interface IUserRepository : IRepository<User>
    {
        Task<PaginatedList<UserIndexDto>> GetAllUsers(int pageNumber, int pageSize);
        Task<User> GetUserById(Guid id);
    }
}