using MediatR;
using Microsoft.AspNetCore.Identity;
using ProPri.Core.Communication.Handlers;
using ProPri.Core.Communication.Messages.Common.Notifications;
using ProPri.Core.Constants;
using ProPri.Core.Domain.ValueObjects;
using ProPri.Users.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace ProPri.Users.Application.Commands
{
    public class UsersCommandHandler : CommandHandler,
        IRequestHandler<EditUserCommand, bool>,
        IRequestHandler<LoginCommand, LoginCommandResult>,
        IRequestHandler<LogoutCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UsersCommandHandler(IMediatorHandler mediatorHandler, IUserRepository userRepository,
                                   UserManager<User> userManager, SignInManager<User> signInManager)
            : base(mediatorHandler)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(request)) return false;

            var performingUser = await _userRepository.GetUserById(request.UserId);
            if (performingUser == null)
            {
                await MediatorHandler.PublishNotification(new DomainNotification("user", "Your user could not be found"));
                return false;
            }

            performingUser.UpdateLastActiveDate();
            
            if (!performingUser.HasClaim(ConstData.ClaimUsersWrite))
            {
                await MediatorHandler.PublishNotification(new DomainNotification("user", "You don't have permission to edit a user"));
                return false;
            }

            var editedUser = await _userRepository.GetUserById(request.Id);
            if (editedUser == null)
            {
                await MediatorHandler.PublishNotification(new DomainNotification("user", "The user you are trying to edit could not be found"));
                return false;
            }

            var name = new PersonName(request.FirstName, request.Surname);
            var role = await _userRepository.GetRoleById(request.RoleId);

            if (editedUser.HasRole(ConstData.RoleManager) &&
                (!request.Active || role.Name != ConstData.RoleManager) &&
                await _userRepository.QtyOfActiveUsersInRole(ConstData.RoleManager) == 1)
            {
                await MediatorHandler.PublishNotification(new DomainNotification("user", $"There must be at least one active user in the role of {ConstData.RoleManager}"));
                return false;
            }

            var updateValid = performingUser.UpdateUser(editedUser, name, request.Email, request.Birthday, request.Active, role);
            
            if (!updateValid)
            {
                await MediatorHandler.PublishNotification(new DomainNotification("user", "You don't have permission to edit this user"));
                return false;
            }

            _userRepository.UpdateUser(performingUser);
            _userRepository.UpdateUser(editedUser);
            return await _userRepository.UnitOfWork.Commit();
        }

        public async Task<LoginCommandResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(request)) return new LoginCommandResult(false);

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                await MediatorHandler.PublishNotification(new DomainNotification("user", "Invalid login or e-mail"));
                return new LoginCommandResult(false);
            }

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, true, false);

            if (result.Succeeded)
                return new LoginCommandResult(true, user.Id);

            await MediatorHandler.PublishNotification(new DomainNotification("user", "Invalid login or e-mail"));
            return new LoginCommandResult(false);
        }

        public async Task<bool> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(request)) return false;

            await _signInManager.SignOutAsync();
            return true;
        }
    }
}