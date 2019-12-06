using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProPri.Core.Communication.Handlers;
using ProPri.Core.Communication.Messages.Common.Notifications;
using ProPri.WebApp.Api.Helpers;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProPri.WebApp.Api.Controllers
{
    public abstract class ApiBaseController : Controller
    {
        protected string LoggedInUser => User.FindFirst(ClaimTypes.NameIdentifier).Value;

        private readonly DomainNotificationHandler _notifications;

        private readonly IMediatorHandler _mediatorHandler;
        protected ApiBaseController(INotificationHandler<DomainNotification> notifications, IMediatorHandler mediatorHandler)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediatorHandler = mediatorHandler;
        }

        protected async Task<IActionResult> Respond(object result)
        {
            if (_notifications.NotificationExists())
                return BadRequest(new ApiResponse(false, null, _notifications.GetNotifications().Select(n => n.Value).ToList()));

            if (result is IComandoResultadoGenerico genericCommandResult)
            {
                return StatusCode(
                    (int)genericCommandResult.CodigoHttp
                    , new ApiResponse(genericCommandResult.Sucesso, genericCommandResult.Resultado, null));
            }

            return Ok(new ApiResponse(true, result, null));
        }
    }
}