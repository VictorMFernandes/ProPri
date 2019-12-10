using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProPri.Core.Communication.Handlers;
using ProPri.Core.Communication.Messages.Common.Notifications;
using ProPri.Users.Application.Commands;
using ProPri.Users.Application.Queries;
using ProPri.WebApp.Mvc.Views.Auth.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ProPri.WebApp.Mvc.Managers;

namespace ProPri.WebApp.Mvc.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IUsersQueries _usersQueries;
        private readonly PageManager _pageManager;

        public AuthController(INotificationHandler<DomainNotification> notifications,
                              IMediatorHandler mediatorHandler,
                              IUsersQueries usersQueries,
                              PageManager pageManager)
            : base(notifications, mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
            _usersQueries = usersQueries;
            _pageManager = pageManager;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            if (_usersQueries.IsSignedIn(User))
                return _pageManager.RedirectToMainPage();

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
                return _pageManager.RedirectToMainPage();

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