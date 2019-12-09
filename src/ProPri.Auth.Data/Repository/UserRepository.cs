using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProPri.Core.Data;
using ProPri.Core.Helpers;
using ProPri.Users.Domain;
using ProPri.Users.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProPri.Users.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper _mapper;
        private readonly UsersContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public UserRepository(UsersContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<UserIndexDto>> GetUsers(int pageNumber, int pageSize)
        {
            var users = await PaginatedList<UserIndexDto>.Create(_context.Users
                .AsNoTracking()
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .Select(u => new UserIndexDto(
                    u.Id, u.Name.ToString(), u.UserRoles.Single().Role.Name)
                ), pageNumber, pageSize);

            return users;
        }

        public async Task<UserFormDto> GetUserById(Guid id)
        {
            var user = await _context.Users.AsNoTracking()
                .Include(u => u.UserRoles)
                .FirstOrDefaultAsync(u => u.Id == id);
            return _mapper.Map<UserFormDto>(user);
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
        }

        public async Task<IEnumerable<RoleIdNameDto>> GetAllRoleIdName()
        {
            var roles = await _context.Roles.AsNoTracking().ToListAsync();
            return _mapper.Map<IEnumerable<RoleIdNameDto>>(roles);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}