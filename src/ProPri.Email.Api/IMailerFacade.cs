using System.Threading.Tasks;

namespace ProPri.Email.Api
{
    public interface IMailerFacade
    {
        Task<bool> SendEmail(string receiverEmail, string receiverName, string subject, string content);
    }
}