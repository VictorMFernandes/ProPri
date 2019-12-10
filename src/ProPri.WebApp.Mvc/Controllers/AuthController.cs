using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProPri.Core.Communication.Handlers;
using ProPri.Core.Communication.Messages.Common.Notifications;
using ProPri.Users.Application.Commands;
using ProPri.Users.Application.Queries;
using ProPri.WebApp.Mvc.Views.Auth.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

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
        public IActionResult Login()
        {
            if (UsersQueries.IsSignedIn(User))
                return RedirectToMainPage();

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

            var result = await _mediatorHandler.SendCommand(loginCommand);

            if (result)
                return RedirectToMainPage();

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

        public IActionResult RecoverPassword(LoginViewModel loginVm)
        {
            return View(loginVm);
        }

        [HttpPost]
        public IActionResult RecoverPassword(string email)
        {
            return View();
        }
    }
}