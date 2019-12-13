using MediatR;
using ProPri.Core.Communication.Handlers;
using ProPri.Core.Communication.Messages.Common.Notifications;
using ProPri.Core.Constants;
using ProPri.Core.Domain.ValueObjects;
using ProPri.Users.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;
using ProPri.Core.Communication.Messages.Common.Events.IntegrationEvents;

namespace ProPri.Users.Application.Commands
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

            var name = new PersonName(request.FirstName, request.Surname);
            var role = await _userRepository.GetRoleById(request.RoleId);
            var createdUser = performingUser.CreateUser(name, request.Email, request.Birthday, role);

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
                await _userRepository.UnitOfWork.Commit();
                await MediatorHandler.PublishEvent(new UserCreatedEvent(createdUser.Id, createdUser.Name.ToString(), tempPassword));
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
            return await _userRepository.UnitOfWork.Commit();
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