using System.Threading.Tasks;
using SendGrid;

namespace TuringBackend.Api.Services
{
    public interface IEmailService
    {
        Task<Response> SendEmail(string email, string subject, string message);
    }
}