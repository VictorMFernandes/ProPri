using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rise.Core.Communication.Handlers;
using Rise.Core.Constants;
using Rise.Core.Data;
using Rise.Core.Helpers;
using Rise.Users.Domain;
using Rise.Users.Domain.Dtos;
using Rise.Users.Domain.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rise.Users.Data.Repositories
{
    public class UserRepository : Repository<UsersContext>, IUserRepository
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        #region Constructors

        public UserRepository(UsersContext context, IMediatorHandler mediatorHandler,
            IMapper mapper, UserManager<User> userManager,
            SignInManager<User> signInManager)
            : base(mediatorHandler, context)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #endregion

        #region User

        public async Task<PaginatedList<UserIndexDto>> GetUsers(UserFilter filter)
        {
            var users = await PaginatedList<UserIndexDto>.Create(Context.Users
                .AsNoTracking()
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .Where(u => !u.IsAdministrator &&
                            EF.Functions.Like(u.NormalizedName, $"%{filter.SearchString}%") &&
                            (filter.ActiveFilter == EActiveFilter.All || filter.ActiveFilter == EActiveFilter.Active && u.Active || filter.ActiveFilter == EActiveFilter.Inactive && !u.Active))
                            .Select(u => new UserIndexDto(
                    u.Id, u.Name, u.UserRoles.Single().Role.Name, u.Active)
                ), filter.PageNumber, filter.PageSize);

            return users;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<UserFormDto> GetUserFormById(Guid id)
        {
            var user = await Context.Users.AsNoTracking()
                .Include(u => u.UserRoles)
                .FirstOrDefaultAsync(u => u.Id == id && !u.IsAdministrator);

            return user == null ? null : _mapper.Map<UserFormDto>(user);
        }

        public async Task<User> GetUserById(Guid id)
        {
            var user = await Context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .ThenInclude(r => r.RoleClaims)
                .FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<IdentityResult> CreateUser(User user, string tempPassword)
        {
            var result = await _userManager.CreateAsync(user, tempPassword);
            return result;
        }

        public void UpdateUser(User user)
        {
            Context.Users.Update(user);
        }

        #endregion

        #region Auth

        public async Task<SignInResult> SignIn(string email, string password)
        {
            return await _signInManager.PasswordSignInAsync(email, password, true, false);
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> ChangePassword(User user, string currentPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        }

        #endregion

        #region Role

        public async Task<IEnumerable<RoleIdNameDto>> GetAllRoleIdName()
        {
            var roles = await Context.Roles.AsNoTracking().Where(r => r.Name != ConstData.RoleAdministrator).ToListAsync();
            return _mapper.Map<IEnumerable<RoleIdNameDto>>(roles);
        }

        public async Task<Role> GetRoleById(Guid id)
        {
            var role = await Context.Roles.AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id && r.Name != ConstData.RoleAdministrator);
            return role;
        }

        #endregion

        #region Claim

        public async Task<bool> HasClaim(Guid userId, string claimValue)
        {
            if (userId == Guid.Empty)
                return false;

            var userRole = await Context.UserRoles
                .AsNoTracking()
                .Include(ur => ur.Role)
                .ThenInclude(r => r.RoleClaims)
                .FirstOrDefaultAsync(ur => ur.UserId == userId);

            var result = userRole?.Role.RoleClaims.Any(rc => rc.ClaimValue == claimValue) ?? false;

            return result;
        }

        #endregion
    }
}