using MediatR;
using Rise.Core.Communication.Handlers;
using Rise.Core.Communication.Messages.Common.Events.IntegrationEvents;
using Rise.Core.Communication.Messages.Common.Notifications;
using Rise.Core.Constants;
using Rise.Users.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Rise.Users.Application.Commands
{
    public class UsersCommandHandler : CommandHandler,
        IRequestHandler<CreateUserCommand, bool>,
        IRequestHandler<EditUserCommand, bool>,
        IRequestHandler<LoginCommand, LoginCommandResult>,
        IRequestHandler<LogoutCommand, bool>,
        IRequestHandler<NewPasswordCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public UsersCommandHandler(IMediatorHandler mediatorHandler,
                                   IUserRepository userRepository)
            : base(mediatorHandler)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(request)) return false;

            var performingUser = await GetPerformingUser(request.LoggedUserId, ConstData.ClaimUsersWrite);
            if (performingUser == null)
                return false;

            var role = await _userRepository.GetRoleById(request.RoleId);
            var createdUser = performingUser.CreateUser(request.Name, request.Email, request.Active, request.Birthday, role);

            if (createdUser == null)
            {
                await MediatorHandler.PublishNotification(new DomainNotification("user", "You don't have permission to create a user with that role"));
                return false;
            }

            _userRepository.UpdateUser(performingUser);
            var tempPassword = createdUser.GenerateTempPassword();
            var createResult = await _userRepository.CreateUser(createdUser, tempPassword);

            if (createResult.Succeeded)
            {
                createdUser.AddEvent(new UserCreatedEvent(createdUser.Id, createdUser.Name, createdUser.Email, tempPassword));
                await _userRepository.Commit();
                return true;
            }

            foreach (var error in createResult.Errors)
            {
                await MediatorHandler.PublishNotification(new DomainNotification("user", error.Description));
            }

            return false;
        }

        public async Task<bool> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(request)) return false;

            var performingUser = await GetPerformingUser(request.LoggedUserId, ConstData.ClaimUsersWrite);
            if (performingUser == null)
                return false;

            var editedUser = await _userRepository.GetUserById(request.Id);
            if (editedUser == null)
            {
                await MediatorHandler.PublishNotification(new DomainNotification("user", "The user you are trying to edit could not be found"));
                return false;
            }

            var role = await _userRepository.GetRoleById(request.RoleId);

            var updateValid = performingUser.UpdateUser(editedUser, request.Name, request.Email, request.Birthday, request.Active, role);

            if (!updateValid)
            {
                await MediatorHandler.PublishNotification(new DomainNotification("user", "You don't have permission to edit this user"));
                return false;
            }

            _userRepository.UpdateUser(performingUser);
            _userRepository.UpdateUser(editedUser);
            return await _userRepository.Commit();
        }

        public async Task<LoginCommandResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(request)) return new LoginCommandResult(false);

            var user = await _userRepository.GetUserByEmail(request.Email);

            if (user == null)
            {
                await MediatorHandler.PublishNotification(new DomainNotification("user", "Invalid login or e-mail"));
                return new LoginCommandResult(false);
            }

            if (!user.Active)
            {
                await MediatorHandler.PublishNotification(new DomainNotification("user", "Your account isn't activated"));
                return new LoginCommandResult(false);
            }

            var loginResult = await _userRepository.SignIn(user.Email, request.Password);

            if (loginResult.Succeeded)
                return new LoginCommandResult(true, user.Id);

            if (loginResult.IsNotAllowed)
                return new LoginCommandResult(false, user.Id, true);

            await MediatorHandler.PublishNotification(new DomainNotification("user", "Invalid login or e-mail"));
            return new LoginCommandResult(false);
        }

        public async Task<bool> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(request)) return false;

            await _userRepository.SignOut();
            return true;
        }

        public async Task<bool> Handle(NewPasswordCommand request, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(request)) return false;

            var user = await _userRepository.GetUserById(request.UserId);

            var result = await _userRepository.ChangePassword(user, request.CurrentPassword, request.Password);

            if (!result.Succeeded)
                return false;

            user.EmailConfirmed = true;
            var signInResult = await _userRepository.SignIn(user.Email, request.Password);

            if (!signInResult.Succeeded)
                return false;

            _userRepository.UpdateUser(user);
            return await _userRepository.Commit();
        }

        public async Task<User> GetPerformingUser(Guid userId, string claim)
        {
            var performingUser = await _userRepository.GetUserById(userId);
            if (performingUser == null)
            {
                await MediatorHandler.PublishNotification(new DomainNotification("user", "Your user could not be found"));
                return null;
            }

            performingUser.UpdateLastActiveDate();

            if (performingUser.HasClaim(claim)) return performingUser;

            await MediatorHandler.PublishNotification(new DomainNotification("user", "You don't have permission to edit a user"));
            return null;
        }
    }
}