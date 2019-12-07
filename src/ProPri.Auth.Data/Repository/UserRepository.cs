using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProPri.Core.Data;
using ProPri.Core.Helpers;
using ProPri.Users.Domain;
using ProPri.Users.Domain.Dtos;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProPri.Users.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UsersContext _context;
        private readonly UserManager<User> _userManager;

        public IUnitOfWork UnitOfWork => _context;

        public UserRepository(UsersContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<PaginatedList<UserIndexDto>> GetAllUsers(int pageNumber, int pageSize)
        {
            var users = await PaginatedList<UserIndexDto>.Create(_context.Users.Select(u => new UserIndexDto
            {
                Id = u.Id,
                Name = u.Name.ToString()
            }), pageNumber, pageSize);

            foreach (var user in users)
            {
                user.Role = await _context.UserRoles.Where(ur => ur.UserId == user.Id).Select()
            }

            return users;
        }

        public async Task<User> GetUserById(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id.ToString());
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}