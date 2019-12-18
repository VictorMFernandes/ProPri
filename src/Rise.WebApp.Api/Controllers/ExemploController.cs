using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rise.Core.Communication.Handlers;
using Rise.Core.Communication.Messages.Common.Notifications;

namespace Rise.WebApp.Api.Controllers
{
    public class ExemploController : ApiBaseController
    {
        protected ExemploController(INotificationHandler<DomainNotification> notifications, IMediatorHandler mediatorHandler) : base(notifications, mediatorHandler)
        {
        }

        [Route("Exemplo/Index")]
        public IActionResult Index()
        {
            return Json(new { teste = "Funcionou" });
        }
    }
}