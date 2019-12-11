using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProPri.Core.Communication.Handlers;
using ProPri.Core.Communication.Messages.Common.Notifications;
using ProPri.Users.Application.Commands;
using ProPri.Users.Application.Queries;
using ProPri.WebApp.Mvc.Views.Auth.ViewModels;
using System.Threading.Tasks;

namespace ProPri.WebApp.Mvc.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IMediatorHandler _mediatorHandler;

        public AuthController(INotificationHandler<DomainNotification> notifications,
                              IMediatorHandler mediatorHandler,
                              IUsersQueries usersQueries)
            : base(notifications, mediatorHandler, usersQueries)
        {
            _mediatorHandler = mediatorHandler;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            if (UsersQueries.IsSignedIn(User))
                return await RedirectToMainPageAsync(LoggedUserId);

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVm)
        {
            var loginCommand = new LoginCommand
            {
                Email = loginVm.Email,
                Password = loginVm.Password
            };

            var loginResult = await _mediatorHandler.SendCommand<LoginCommand, LoginCommandResult>(loginCommand);

            if (loginResult.Success)
                return await RedirectToMainPageAsync(loginResult.UserId);

            return View(loginVm);
        }

        public async Task<IActionResult> Logout()
        {
            await _mediatorHandler.SendCommand(new LogoutCommand());
            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult RecoverPassword(LoginViewModel loginVm)
        {
            return View(loginVm);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult RecoverPassword(string email)
        {
            return View();
        }
    }
}