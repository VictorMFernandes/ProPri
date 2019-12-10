using MediatR;
using Microsoft.AspNetCore.Identity;
using ProPri.Core.Communication.Handlers;
using ProPri.Core.Communication.Messages.Common.Notifications;
using ProPri.Core.Constants;
using ProPri.Core.Domain.ValueObjects;
using ProPri.Users.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProPri.Users.Application.Commands
{
    public class UsersCommandHandler : CommandHandler,
        IRequestHandler<EditUserCommand, bool>,
        IRequestHandler<LoginCommand, bool>,
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

            var user = await GetLoggedUser(request.UserId, ConstData.RoleManager);
            if (user == null)
                return false;

            var editedUser = await _userManager.FindByIdAsync(request.Id.ToString());
            if (editedUser == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("user", "The user you are trying to edit could not be found"));
                return false;
            }

            var name = new PersonName(request.FirstName, request.Surname);
            editedUser.Update(name, request.Email, request.Active, request.Birthday);

            _userRepository.UpdateUser(editedUser);
            return await _userRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(request)) return false;

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
                return false;

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, true, false);
            
            return result.Succeeded;
        }

        public async Task<bool> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(request)) return false;

            await _signInManager.SignOutAsync();
            return true;
        }

        private async Task<User> GetLoggedUser(Guid userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("user", "Your user could not be found"));
                return null;
            }

            user.UpdateLastActiveDate();
            await _userRepository.UnitOfWork.Commit();

            if (await _userManager.IsInRoleAsync(user, role)) return user;

            await _mediatorHandler.PublishNotification(new DomainNotification("user", "You have no permission to perform this action"));
            return null;
        }
    }
}