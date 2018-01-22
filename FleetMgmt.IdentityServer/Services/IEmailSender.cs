using System.Threading.Tasks;

namespace FleetMgmt.IdentityServer.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
