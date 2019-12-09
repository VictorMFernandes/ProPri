using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProPri.Core.Communication.Handlers;
using ProPri.Core.Communication.Messages.Common.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace ProPri.WebApp.Mvc.Controllers
{
    public class BaseController : Controller
    {
        private readonly DomainNotificationHandler _notifications;
        private readonly IMediatorHandler _mediatorHandler;

        protected Guid LoggedUserId => new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));

        protected BaseController(INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediatorHandler)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediatorHandler = mediatorHandler;
        }

        protected bool ValidOperation()
        {
            return !_notifications.NotificationExists();
        }

        protected IEnumerable<string> GetErrorMessages()
        {
            return _notifications.GetNotifications().Select(c => c.Value).ToList();
        }

        protected void NotificarErro(string codigo, string mensagem)
        {
            _mediatorHandler.PublishNotification(new DomainNotification(codigo, mensagem));
        }
    }
}