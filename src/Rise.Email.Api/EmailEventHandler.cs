using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Rise.Core.Communication.Messages.Common.Events.IntegrationEvents;

namespace Rise.Email.Api
{
    public class EmailEventHandler : INotificationHandler<UserCreatedEvent>
    {
        private readonly IMailerFacade _mailer;

        public EmailEventHandler(IMailerFacade mailer)
        {
            _mailer = mailer;
        }

        public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            var content = new StringBuilder(EmailTemplates.ResourceManager.GetString("Welcome"))
                .Replace("[created-user-name]", notification.CreatedUserName)
                .Replace("[created-user-password]", notification.CreatedUserPassword)
                .Replace("[website-login-page]", "https://localhost:44398/Auth/login");

            await _mailer.SendEmail(notification.CreatedUserEmail, notification.CreatedUserName, "Welcome from Rise!", content.ToString());
        }
    }
}