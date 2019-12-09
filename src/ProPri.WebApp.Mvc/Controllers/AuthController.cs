using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProPri.Core.Communication.Handlers;
using ProPri.Core.Communication.Messages.Common.Notifications;
using ProPri.Core.Domain.ValueObjects;
using ProPri.Users.Application.Commands;
using ProPri.WebApp.Mvc.Views.Auth.ViewModels;

namespace ProPri.WebApp.Mvc.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IMediatorHandler _mediatorHandler;

        public AuthController(INotificationHandler<DomainNotification> notifications,
                              IMediatorHandler mediatorHandler)
            : base(notifications, mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        public IActionResult Login()
        {
            return View();
        }

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
                return RedirectToAction("Index", "Users");

            return View(loginVm);
        }

        public IActionResult Logout()
        {
            return RedirectToAction("Login");
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