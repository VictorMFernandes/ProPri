using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rise.Core.Communication.Handlers;
using Rise.Core.Communication.Messages.Common.Notifications;
using Rise.Core.Constants;
using Rise.Users.Application.Queries;
using Rise.WebApp.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rise.WebApp.Mvc.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        private readonly DomainNotificationHandler _notifications;
        private readonly IMediatorHandler _mediatorHandler;
        protected readonly IUsersQueries UsersQueries;

        protected Guid LoggedUserId => AuthorizationExtensions.GetLoggedUserId(User);

        protected BaseController(INotificationHandler<DomainNotification> notifications,
                                 IMediatorHandler mediatorHandler,
                                 IUsersQueries usersQueries)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediatorHandler = mediatorHandler;
            UsersQueries = usersQueries;
        }

        protected bool ValidOperation()
        {
            return !_notifications.NotificationExists();
        }

        private IEnumerable<string> GetErrorMessages()
        {
            return _notifications.GetNotifications().Select(c => c.Value).ToList();
        }

        protected void NotifyError(string code, string message)
        {
            _mediatorHandler.PublishNotification(new DomainNotification(code, message));
        }

        protected async Task<RedirectToActionResult> RedirectToMainPageAsync(Guid userId)
        {
            var result = await UsersQueries.IsAuthorized(userId, ConstData.ClaimUsersRead);

            var controller = result ? "Users" : "Students";

            var claims = User.Claims.ToList();
            return RedirectToAction("Index", controller);
        }
    }
}