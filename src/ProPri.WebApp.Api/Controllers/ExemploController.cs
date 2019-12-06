using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProPri.Core.Communication.Handlers;
using ProPri.Core.Communication.Messages.Common.Notifications;

namespace ProPri.WebApp.Api.Controllers
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