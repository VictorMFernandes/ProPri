using ProPri.Email.Api;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;
using System.Threading.Tasks;

namespace Rise.Email.AntiCorruption
{
    public class SendGridMailerFacade : IMailerFacade
    {
        private readonly SendGridClient _client;
        private readonly EmailAddress _sender;

        public SendGridMailerFacade(SendGridOptions opt)
        {
            _client = new SendGridClient(opt.ApiKey);
            _sender = new EmailAddress(opt.SenderEmail);
        }

        public async Task<bool> SendEmail(string receiverEmail, string receiverName, string subject, string content)
        {
            var receiver = new EmailAddress(receiverEmail, receiverName);
            var msg = MailHelper.CreateSingleEmail(_sender, receiver, subject, string.Empty, content);
            var response = await _client.SendEmailAsync(msg);

            return response.StatusCode == HttpStatusCode.Accepted;
        }
    }
}