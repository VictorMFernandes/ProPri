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

        #region User

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

        public async Task<UserFormDto> GetUserByIdWithUserRoles(Guid id)
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

        #endregion

        #region Role

        public async Task<IEnumerable<RoleIdNameDto>> GetAllRoleIdName()
        {
            var roles = await _context.Roles.AsNoTracking().ToListAsync();
            return _mapper.Map<IEnumerable<RoleIdNameDto>>(roles);
        }

        #endregion

        #region Claim

        public async Task<bool> HasClaim(Guid userId, string claimValue)
        {
            if (userId == Guid.Empty)
                return false;

            var userRole = await _context.UserRoles
                .AsNoTracking()
                .Include(ur => ur.Role)
                .ThenInclude(r => r.RoleClaims)
                .FirstOrDefaultAsync(ur => ur.UserId == userId);

            var result = userRole.Role.RoleClaims.Any(rc => rc.ClaimValue == claimValue);

            return result;
        }

        #endregion

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}