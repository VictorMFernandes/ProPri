using System.Threading.Tasks;

namespace Rise.Email.Api
{
    public interface IMailerFacade
    {
        Task<bool> SendEmail(string receiverEmail, string receiverName, string subject, string content);
    }
}