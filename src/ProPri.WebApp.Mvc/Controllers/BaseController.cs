using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProPri.Core.Communication.Handlers;
using ProPri.Core.Communication.Messages.Common.Notifications;
using ProPri.Core.Constants;
using ProPri.Users.Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProPri.WebApp.Mvc.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        private readonly DomainNotificationHandler _notifications;
        private readonly IMediatorHandler _mediatorHandler;
        protected readonly IUsersQueries UsersQueries;

        protected Guid LoggedUserId
        {
            get
            {
                var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
                return id == null ? Guid.Empty : new Guid(id);
            }
        }

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

            return RedirectToAction("Index", controller);
        }
    }
}